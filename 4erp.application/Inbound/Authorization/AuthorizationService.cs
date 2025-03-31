
using System.Threading.Tasks;
using _4erp.api.entities;
using _4erp.api.entities.person;
using _4erp.api.entities.status;
using _4erp.domain.Ports;
using _4erp.domain.repositories;
using _4erp.domain.Services.Users;
using _4erp.infrastructure.Security.JWT;


namespace _4erp.application.Inbound.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;
        private readonly IGenericRepository<Role> _roleRepository;

        private readonly IUserRepository _userRepository;

        public AuthorizationService(
            IUserService userService,
            IJWTService jwtService,
            IGenericRepository<Role> roleRepository,
            IUserRepository userRepository)
        {
            _userService = userService;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<Authorized> Authorization(Authorization authorization)
        {

            if (authorization.Email is null)
                throw new Exception("E-mail não preenchido!");

            if (authorization.Password is null)
                throw new Exception("E-mail ou Senha não preenchido!");

            var userFound = await _userService.FindByEmailAsync(authorization.Email);

            if (userFound is null)
                throw new Exception("Nenhum usuário encontrado com este e-mail");

            bool authorized = BCrypt.Net.BCrypt.Verify(
                authorization.Password, userFound.Password);

            if (!authorized)
                throw new Exception("Não autorizado! E-mail ou Senha não preenchido!");

            string token = _jwtService.WriteToken(
                userFound.Id.ToString(),
                new JWTRole()
            );

            return new Authorized(token);
        }

        public async Task<Authorized> Register(Authorization authorization)
        {
            if (authorization.Person is null)
                throw new Exception("Por favor preencha dos dados corretamente!");

            string alias = authorization.Person.Type == (int)AuthorizationRoleEnum.COMPANY
                ? "administrator:company:system:*"
                : "administrator:person:system:*";


            var role = await _roleRepository.FindFirstAsync(
                    u => u.Alias.Contains(alias)
            );

            if (role is null)
                throw new Exception("Nenhuma role encontrada!");


            if (authorization.Email is null)
                throw new Exception("E-mail não preenchido!");

            if (authorization.Password is null)
                throw new Exception("E-mail ou Senha não preenchido!");

            var userFound = await _userService.FindByEmailAsync(authorization.Email);

            if (userFound is not null)
                throw new Exception("Já existe um usuário cadastrado com este e-mail!");


            var user = new User();
            user.Email = authorization.Email;
            user.Password = authorization.Password;
            user.Role = role;


            user.Person = new Person
            {
                FirstName = authorization.Person.FirstName,
                LastName = authorization.Person.LastName,
                LegalName = authorization.Person.LegalName,
                Type = authorization.Person.Type,
                FantasyName = authorization.Person.FantasyName,
                TaxId = authorization.Person.TaxId,
                Bio = new Bio()
                {
                    About = "",
                    Linkedin = "",
                    Resume = "",
                    WebSite = "",
                    Experiences = [],
                    Educations = []

                },
                Phone = new Phone
                {
                    DDD = "",
                    DDI = "",
                    Number = ""
                }
            };

            await _userService.AddAsync(
                user
            );

            string token = _jwtService.WriteToken(
                user.Id.ToString(),
                new JWTRole()
            );

            return new Authorized(token);

        }
    }
}