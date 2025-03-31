using Microsoft.EntityFrameworkCore;
using _4erp.api.entities;
using _4erp.api.entities.ocupation;
using _4erp.api.entities.vacancy;
using _4erp.api.entities.person;
using _4erp.api.entities.skill;
using _4erp.api.entities.status;
using _4erp.api.entities.candidature;
using System.Data;

namespace _4erp.infrastructure.data.context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ocupation> Ocupations { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateTables(modelBuilder);
            CreateData(modelBuilder);
        }

        private void CreateTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Vacancy>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Phone>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Bio>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);


            modelBuilder.Entity<Status>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Scope>()
                .HasOne(s => s.Role)
                .WithMany(r => r.Scopes)
                .HasForeignKey(s => s.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Experience>()
                .HasOne(s => s.Bio)
                .WithMany(r => r.Experiences)
                .HasForeignKey(s => s.BioId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Education>()
                .HasOne(s => s.Bio)
                .WithMany(r => r.Educations)
                .HasForeignKey(s => s.BioId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Person>()
                 .HasOne(v => v.Bio)
                 .WithMany(o => o.Persons)
                 .HasForeignKey(v => v.BioId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Person>()
                 .HasOne(v => v.Phone)
                 .WithMany(o => o.Persons)
                 .HasForeignKey(v => v.PhoneId)
                 .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Person>()
                .HasMany(p => p.Skills)
                .WithMany(s => s.Persons)
                .UsingEntity<Dictionary<string, object>>(
                    "4erp_person_skill",
                    j => j.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                    j => j.HasOne<Person>().WithMany().HasForeignKey("PersonId")
                );

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Ocupation)
                .WithMany(o => o.Vacancies)
                .HasForeignKey(v => v.OcupationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Status)
                .WithMany(o => o.Vacancies)
                .HasForeignKey(v => v.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Person)
                .WithMany(p => p.Vacancies)
                .HasForeignKey(v => v.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vacancy>()
                .HasMany(v => v.Skills)
                .WithMany(s => s.Vacancies)
                .UsingEntity<Dictionary<string, object>>(
                    "4erp_vacancy_skill",
                    j => j.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                    j => j.HasOne<Vacancy>().WithMany().HasForeignKey("VacancyId")
                );

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Person)
                .WithMany(p => p.Candidatures)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Vacancy)
                .WithMany(v => v.Candidatures)
                .HasForeignKey(c => c.VacancyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Status)
                .WithMany(s => s.Candidatures)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void CreateData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Status>().HasData(
                new Status
                {
                    Id = new Guid("5eda1e3c-1b97-480c-bee4-9ea0c61859c5"),
                    Name = "waiting",
                    Slug = "candidature_status",
                    Description = "Aguardando análise"
                },
                new Status
                {
                    Id = new Guid("29d6e75a-3808-482d-9316-493a73dc6fe0"),
                    Name = "candidate_evaluation",
                    Slug = "candidature_status",
                    Description = "Avaliando candidato"
                },
                new Status
                {
                    Id = new Guid("711df75d-cebf-46e9-8873-6a8ce6670b5b"),
                    Name = "interview",
                    Slug = "candidature_status",
                    Description = "Entrevista"
                },
                new Status
                {
                    Id = new Guid("0558afb1-4a6e-46fb-bd0d-2d14b756b348"),
                    Name = "rejected",
                    Slug = "candidature_status",
                    Description = "Rejeitado"
                },
                new Status
                {
                    Id = new Guid("735b983b-db8f-4ece-a96c-6007fb1de5b9"),
                    Name = "approved",
                    Slug = "candidature_status",
                    Description = "Aprovado"
                },
                new Status
                {
                    Id = new Guid("44147d27-ff38-413f-8187-cce3e5d603f4"),
                    Name = "candidate_evaluation",
                    Slug = "candidature_status",
                    Description = "Avaliação técnica"
                },
                new Status
                {
                    Id = new Guid("bb0bc2c3-62c4-4159-a680-5a45226ab7b1"),
                    Name = "vacancy_waiting",
                    Slug = "vacancy_status",
                    Description = "Em breve"
                },
                new Status
                {
                    Id = new Guid("b1cd5678-0f19-4b2f-87ff-f1a0d75421f2"),
                    Name = "vacancy_in_progress",
                    Slug = "vacancy_status",
                    Description = "Em andamento"
                },
                new Status
                {
                    Id = new Guid("e795eaaf-e08c-430e-a652-242a8dcf886d"),
                    Name = "vacancy_done",
                    Slug = "vacancy_status",
                    Description = "Finalizada"
                },
                new Status
                {
                    Id = new Guid("e04f8400-f060-4c04-8e75-eb540cc3baba"),
                    Name = "vacancy_canceled",
                    Slug = "vacancy_status",
                    Description = "Cancelada"
                }
            );

            modelBuilder.Entity<Skill>().HasData(

                new Skill
                {
                    Id = new Guid("4b867bb5-eb6b-41cc-87a6-5041927be496"),
                    Name = "C#",
                    Description = "Linguagem de programação orientada a objetos utilizada para desenvolvimento web, desktop e mobile."
                },
                new Skill
                {
                    Id = new Guid("49dae8b3-25b0-4264-a4f2-0df5932101bd"),
                    Name = "Python",
                    Description = "Linguagem versátil usada para desenvolvimento web, automação, ciência de dados e inteligência artificial."
                },
                new Skill
                {
                    Id = new Guid("48aa83d8-bbdf-45c5-8909-0155d254e5f4"),
                    Name = "Typescript",
                    Description = "Linguagem de programação essencial para desenvolvimento web, utilizada no frontend e backend."
                },
                new Skill
                {
                    Id = new Guid("718e2ce0-a235-432f-bd9f-9dbe272d5924"),
                    Name = "Java",
                    Description = "Linguagem popular para desenvolvimento corporativo, aplicações móveis (Android) e sistemas distribuídos."
                },
                new Skill
                {
                    Id = new Guid("1374000b-ddc6-4668-8e0c-f75f302106a2"),
                    Name = "Go",
                    Description = "Linguagem moderna desenvolvida pelo Google, focada em performance e concorrência."
                },
                new Skill
                {
                    Id = new Guid("3b0c221a-9de4-4624-9a7b-33c6705a644e"),
                    Name = "Rust",
                    Description = "Linguagem segura e eficiente, utilizada para sistemas embarcados, segurança e alto desempenho."
                }
            );

            modelBuilder.Entity<Ocupation>().HasData(
                new Ocupation
                {
                    Id = new Guid("26af6758-9513-4f84-b9b2-d3a421d56996"),
                    Name = "Engenheiro de Software",
                    Description = "Engenheiro de Software"
                },
                new Ocupation
                {
                    Id = new Guid("05752402-43cf-42bf-aa56-a553d055ce01"),
                    Name = "Arquiteto de Software",
                    Description = "Arquiteto de Software"
                },
                new Ocupation
                {
                    Id = new Guid("bef71141-4859-46cb-9880-7a83d1532d14"),
                    Name = "Desenvolvedor de Software",
                    Description = "Desenvolvedor de Software"
                },
                new Ocupation
                {
                    Id = new Guid("f0173a5a-473f-4e86-915b-8bd63ed68903"),
                    Name = "Líder Técnico",
                    Description = "Líder Técnico"
                },
                new Ocupation
                {
                    Id = new Guid("bad88f10-d1e5-4807-9f2c-852e25276b38"),
                    Name = "QA",
                    Description = "QA"
                },
                new Ocupation
                {
                    Id = new Guid("3fdaa34f-67e4-4325-b9ba-d0ea9a351579"),
                    Name = "Analista de Sistemas",
                    Description = "Analista de Sistemas"
                }
            );

            // ADMINISTRADOR

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c"),
                Name = "Administrador",
                Alias = "administrator:system:*",
                Description = "Administrador do sistema"
            });

            // ADMINISTRADOR

            modelBuilder.Entity<Scope>().HasData(
                new Scope
                {
                    Id = new Guid("2025e44f-a232-43a5-9b8c-769fbd18a06f"),
                    Name = "Criador de vagas",
                    Alias = "administrator:vacancy:create",
                    Description = "Pode criar vagas no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("4852b3ab-dd40-47a6-9548-643ae0f6967e"),
                    Name = "Leitor de vagas",
                    Alias = "administrator:vacancy:read",
                    Description = "Pode ver vagas no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("b2d5653f-4e4c-4ec1-94f9-bd458b72d27a"),
                    Name = "Editor de vagas",
                    Alias = "administrator:vacancy:editor",
                    Description = "Pode editar vagas no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("72bdf067-cc4b-4e36-983b-cb1e7284af48"),
                    Name = "Removedor de vagas",
                    Alias = "administrator:vacancy:remove",
                    Description = "Pode remover vagas no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },

                new Scope
                {
                    Id = new Guid("bdefc049-c893-4871-90e4-5a69298b5e05"),
                    Name = "Pode remover empresas",
                    Alias = "administrator:company:remove",
                    Description = "Pode remover empresas no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("ef924721-b753-4997-b94f-74ea97e3a3d3"),
                    Name = "Pode criar uma nova empresa",
                    Alias = "administrator:company:create",
                    Description = "Pode criar uma nova empresa",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("8cb9c7e0-ba1a-452a-b0b3-49cc930a049c"),
                    Name = "Pode editar uma nova empresa",
                    Alias = "administrator:company:editor",
                    Description = "Pode editar uma nova empresa no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("cac88890-5651-4548-8bc8-71683a9dac7e"),
                    Name = "Pode ler empresas",
                    Alias = "administrator:company:read",
                    Description = "Pode ler empresas no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },

                new Scope
                {
                    Id = new Guid("f48f60d8-ea44-4616-beb2-c031aca31a3d"),
                    Name = "Pode remover empresas",
                    Alias = "administrator:user:remove",
                    Description = "Pode remover uma nova empresa no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("4a5a2b1d-f0e2-493d-8231-e975a295006b"),
                    Name = "Pode criar uma nova empresa",
                    Alias = "administrator:user:create",
                    Description = "Pode criar uma nova empresa no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("b4eff319-159c-4cc9-bcb1-0b36c574900f"),
                    Name = "Pode editar um novo usuários",
                    Alias = "administrator:user:editor",
                    Description = "Pode editar um novo usuários",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("b2d9a2bf-62e1-4ce2-b5a5-2c9d6002d01c"),
                    Name = "Pode ler usuários",
                    Alias = "administrator:user:read",
                    Description = "Pode ler usuários no sistema",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                },
                new Scope
                {
                    Id = new Guid("98593af7-4cf6-4cfd-8947-bc6cfae07229"),
                    Name = "Pode editar suas configurações de conta",
                    Alias = "administrator:account:edit",
                    Description = "Pode editar suas configurações de conta",
                    RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
                }
                
            );

            // COMPANY

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b"),
                Name = "Company",
                Alias = "administrator:company:system:*",
                Description = "Administrador do sistema"
            });

            // COMPANY

            modelBuilder.Entity<Scope>().HasData(
                new Scope
                {
                    Id = new Guid("6b7fc3b4-6c16-41ee-a6ab-0c7dfd21d41e"),
                    Name = "Criador de vagas",
                    Alias = "administrator:company:vacancy:create",
                    Description = "Pode criar vagas no sistema",
                    RoleId = new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b")
                },
                new Scope
                {
                    Id = new Guid("c5878d7a-c03a-4a88-a952-68d7e1d07b0a"),
                    Name = "Leitor de vagas",
                    Alias = "administrator:company:vacancy:read",
                    Description = "Pode ver vagas no sistema",
                    RoleId = new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b")
                },
                new Scope
                {
                    Id = new Guid("a490ea3e-ce26-4d2d-b2a2-77a28cc824cb"),
                    Name = "Editor de vagas",
                    Alias = "administrator:company:vacancy:editor",
                    Description = "Pode editar vagas no sistema",
                    RoleId = new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b")
                },
                new Scope
                {
                    Id = new Guid("e9c764a4-5139-43a1-8d20-a613fe5bf9d3"),
                    Name = "Removedor de vagas",
                    Alias = "administrator:company:vacancy:remove",
                    Description = "Pode remover vagas no sistema",
                    RoleId = new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b")
                },
                new Scope
                {
                    Id = new Guid("3f87c5ea-edc8-4826-b67b-396a0d134bef"),
                    Name = "Pode editar suas configurações de conta",
                    Alias = "administrator:company:account:edit",
                    Description = "Pode editar suas configurações de conta",
                    RoleId = new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b")
                }
            );


            // PERSON
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16"),
                Name = "Person",
                Alias = "administrator:person:system:*",
                Description = "Usuario do sistema"
            });


            // PERSON
            modelBuilder.Entity<Scope>().HasData(
                new Scope
                {
                    Id = new Guid("44a3dc2f-1810-4957-8bd3-0e526ee8d44f"),
                    Name = "Pode ver vagas disponíveis",
                    Alias = "administrator:person:vacancy:view",
                    Description = "Pode criar vagas no sistema",
                    RoleId = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16")
                },
                new Scope
                {
                    Id = new Guid("b74b8e82-0ba6-4c36-a34d-7076862c8882"),
                    Name = "Pode ver candidaturas feitas a vagas",
                    Alias = "administrator:person:candidate:read",
                    Description = "Pode ver vagas no sistema",
                    RoleId = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16")
                },
                new Scope
                {
                    Id = new Guid("9d631e92-5e18-40c5-bb62-a76ef782a77e"),
                    Name = "Pode se candidatar a uma vaga",
                    Alias = "administrator:person:candidate:create",
                    Description = "Pode editar vagas no sistema",
                    RoleId = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16")
                },
                new Scope
                {
                    Id = new Guid("f7bf4f05-4268-428a-b225-1a67e2bf06af"),
                    Name = "Pode remover sua candidatura de uma vaga",
                    Alias = "administrator:person:candidate:remove",
                    Description = "Pode remover vagas no sistema",
                    RoleId = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16")
                },
                new Scope
                {
                    Id = new Guid("a77e474c-6047-4914-941d-29babbbe07b0"),
                    Name = "Pode editar suas configurações de conta",
                    Alias = "administrator:person:account:edit",
                    Description = "Pode editar suas configurações de conta",
                    RoleId = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16")
                }
            );




            modelBuilder.Entity<Person>().HasData(new Person
            {
                Id = new Guid("ebab34b5-3543-4c53-a3cc-03bf86567081"),
                FirstName = "",
                LastName = "",
                TaxId = "18120830000119",
                FantasyName = "4ERP",
                LegalName = "4ERP",
                Type = -1,
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("36374eca-40aa-4a2f-9310-4ba40580bc21"),
                Email = "administrator@4erp.io",
                Password = "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu",
                PersonId = new Guid("ebab34b5-3543-4c53-a3cc-03bf86567081"),
                RoleId = new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c")
            });

            modelBuilder.Entity<Bio>().HasData(new Bio
            {
                Id = new Guid("91597911-94ee-42ef-b1c4-c0ce0e9fe883"),
                About = "A Infinity é uma empresa dedicada à excelência na construção de softwares e na engenharia de soluções tecnológicas. Com uma abordagem inovadora e personalizada, a Infinity alia expertise técnica e visão estratégica para transformar desafios complexos em oportunidades de crescimento para seus clientes. Nossa missão é desenvolver sistemas que impulsionem a eficiência operacional e a competitividade, adotando metodologias ágeis e tecnologias de ponta para criar soluções robustas e escaláveis. Valorizamos a criatividade, o comprometimento e a busca constante pela melhoria contínua, pilares que nos permitem entregar projetos com alta qualidade e impacto no mercado. Na Infinity, acreditamos que a transformação digital é essencial para a evolução dos negócios, e por isso, investimos continuamente na capacitação de nossos profissionais e na atualização de nossas práticas. Nosso time multidisciplinar trabalha em sinergia, integrando conhecimento técnico e visão de negócio para oferecer soluções completas que atendem às demandas específicas de cada projeto. Através de uma gestão transparente e orientada por resultados, a Infinity consolida sua posição como referência no setor, sempre focada em promover a inovação e contribuir para o sucesso dos nossos parceiros e clientes. Com compromisso e dedicação, seguimos construindo o futuro da tecnologia, transformando ideias em soluções que impulsionam o progresso.",
                Resume = "A Infinity é uma empresa dedicada à excelência na construção de softwares e na engenharia de soluções tecnológicas. Com uma abordagem inovadora e personalizada, a Infinity alia expertise técnica e visão estratégica para transformar desafios complexos em oportunidades de crescimento para seus clientes. Nossa missão é desenvolver sistemas que impulsionem a eficiência operacional e a competitividade, adotando metodologias ágeis e tecnologias de ponta para criar soluções robustas e escaláveis. Valorizamos a criatividade, o comprometimento e a busca constante pela melhoria contínua, pilares que nos permitem entregar projetos com alta qualidade e impacto no mercado. Na Infinity, acreditamos que a transformação digital é essencial para a evolução dos negócios, e por isso, investimos continuamente na capacitação de nossos profissionais e na atualização de nossas práticas. Nosso time multidisciplinar trabalha em sinergia, integrando conhecimento técnico e visão de negócio para oferecer soluções completas que atendem às demandas específicas de cada projeto. Através de uma gestão transparente e orientada por resultados, a Infinity consolida sua posição como referência no setor, sempre focada em promover a inovação e contribuir para o sucesso dos nossos parceiros e clientes. Com compromisso e dedicação, seguimos construindo o futuro da tecnologia, transformando ideias em soluções que impulsionam o progresso.",
                Linkedin = "http://linkedin.com/infinity",
                WebSite = "http://linkedin.com/infinity"
            });

            modelBuilder.Entity<Phone>().HasData(new Phone
            {
                Id = new Guid("507accd1-020a-40fe-89c1-73d329133d2a"),
                DDI = "+55",
                DDD = "62",
                Number = "984887715",
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                Id = new Guid("b3ff401f-0c41-47de-a3c8-0d2c5b65e787"),
                FirstName = "",
                LastName = "",
                TaxId = "18120830000118",
                FantasyName = "Infinity",
                LegalName = "Infinity",
                BioId = new Guid("91597911-94ee-42ef-b1c4-c0ce0e9fe883"),
                PhoneId = new Guid("507accd1-020a-40fe-89c1-73d329133d2a"),
                Type = 0,
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("e2920e37-1fb6-4f23-960e-2ce8a1017c09"),
                Email = "infinity@4erp.io",
                Password = "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu",
                PersonId = new Guid("b3ff401f-0c41-47de-a3c8-0d2c5b65e787"),
                RoleId = new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b")
            });


            // Usuário Gabriel
            modelBuilder.Entity<Bio>().HasData(new Bio
            {
                Id = new Guid("9b8717ca-9565-4c6d-951f-64e5e9df0ad5"),
                About = "Olá, me Chamo Gabriel, tenho 33 anos, sou entusiasta em tecnologia, apaixonado por computação e também em escovar bit. Minha carreira na área de TI começou ainda novo, com 13 anos descobri o mundo da computação e como ela poderia transformar meu futuro, de forma incansável, inapelável, incansável busquei aprender diveras áreas como infraestrutura TI, Programação, Vários níveis da Arquitetura de Computadores e Software. Atualmente me dedico a alimentar comunidades Open desenvolvendo ativos de infraestrutura e desenvolvendo startups.",
                Resume = "Trabalhei em grandes empresas como, Invent Software, Trinus Bank, BlueTech, Accenture, Sensedia, Banco BV, Banco Ouribank, Burgerking, Starbucks",
                Linkedin = "http://linkedin.com/gabriel-lima",
                WebSite = "http://linkedin.com/gabriel-lima"
            });

            modelBuilder.Entity<Experience>().HasData(new Experience
            {
                Id = new Guid("4eb08c7e-e56b-49ff-b074-8e863b083738"),
                Name = "Invent Software",
                Description = "Atuei como desenvoledor por 2 anos, onde pude contribuir na construção de aplicações em SAPUI5.",
                BioId = new Guid("9b8717ca-9565-4c6d-951f-64e5e9df0ad5"),
                Current = false,
            });

            modelBuilder.Entity<Phone>().HasData(new Phone
            {
                Id = new Guid("5a44e663-ef90-4fbb-a61e-0633b1dae5f9"),
                DDI = "+55",
                DDD = "62",
                Number = "984887715",
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                Id = new Guid("438d2927-d1d6-442d-97b7-d4d41a67fc35"),
                FirstName = "Gabriel",
                LastName = "Borges Da Silva",
                TaxId = "03931830179",
                FantasyName = "",
                LegalName = "",
                BioId = new Guid("9b8717ca-9565-4c6d-951f-64e5e9df0ad5"),
                PhoneId = new Guid("5a44e663-ef90-4fbb-a61e-0633b1dae5f9"),
                Type = 1,
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("48a29331-9c60-42a9-ad70-e69fc6bc83c2"),
                Email = "gabriel@4erp.io",
                Password = "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu",
                PersonId = new Guid("438d2927-d1d6-442d-97b7-d4d41a67fc35"),
                RoleId = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16")
            });



            // Usuário Alice
            modelBuilder.Entity<Bio>().HasData(new Bio
            {
                Id = new Guid("7f9c8d7f-5264-45a7-894d-889a25e2d069"),
                About = "Olá, me Chamo Alice, tenho 22 anos, sou entusiasta em tecnologia, apaixonado por computação e também em escovar bit. Minha carreira na área de TI começou ainda novo, com 13 anos descobri o mundo da computação e como ela poderia transformar meu futuro, de forma incansável, inapelável, incansável busquei aprender diveras áreas como infraestrutura TI, Programação, Vários níveis da Arquitetura de Computadores e Software. Atualmente me dedico a alimentar comunidades Open desenvolvendo ativos de infraestrutura e desenvolvendo startups.",
                Resume = "Trabalhei em grandes empresas como, Invent Software, Trinus Bank, BlueTech, Accenture, Sensedia, Banco BV, Banco Ouribank, Burgerking, Starbucks",
                Linkedin = "http://linkedin.com/gabriel-lima",
                WebSite = "http://linkedin.com/gabriel-lima"
            });

            modelBuilder.Entity<Phone>().HasData(new Phone
            {
                Id = new Guid("c1808532-d4d3-4429-bb35-69b4c4f50c9d"),
                DDI = "+55",
                DDD = "62",
                Number = "984887715",
            });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                Id = new Guid("05c80ccd-8c9a-4aa2-9dd5-b80e2e30030f"),
                FirstName = "Alice",
                LastName = "Borges Azevedo Lima",
                TaxId = "03931830179",
                FantasyName = "",
                LegalName = "",
                BioId = new Guid("7f9c8d7f-5264-45a7-894d-889a25e2d069"),
                PhoneId = new Guid("c1808532-d4d3-4429-bb35-69b4c4f50c9d"),
                Type = 1,
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("812fefd5-98c7-4257-a609-3eeeb0ac549d"),
                Email = "alice@4erp.io",
                Password = "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu",
                PersonId = new Guid("05c80ccd-8c9a-4aa2-9dd5-b80e2e30030f"),
                RoleId = new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16")
            });
        }
    }
}
