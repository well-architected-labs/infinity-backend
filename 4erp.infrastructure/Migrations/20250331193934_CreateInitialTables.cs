using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _4erp.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "4erp_ocupation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_ocupation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "4erp_phone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DDI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_phone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "4erp_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "4erp_skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "4erp_status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "4erp_scope",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_scope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_4erp_scope_4erp_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "4erp_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "4erp_bio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linkedin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_bio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "4erp_education",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_4erp_education_4erp_bio_BioId",
                        column: x => x.BioId,
                        principalTable: "4erp_bio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "4erp_experience",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Current = table.Column<bool>(type: "bit", nullable: false),
                    BioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_4erp_experience_4erp_bio_BioId",
                        column: x => x.BioId,
                        principalTable: "4erp_bio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "4erp_person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FantasyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_4erp_person_4erp_bio_BioId",
                        column: x => x.BioId,
                        principalTable: "4erp_bio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_4erp_person_4erp_phone_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "4erp_phone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "4erp_person_skill",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_person_skill", x => new { x.PersonId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_4erp_person_skill_4erp_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "4erp_person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_4erp_person_skill_4erp_skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "4erp_skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "4erp_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_4erp_user_4erp_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "4erp_person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_4erp_user_4erp_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "4erp_role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "4erp_vacancy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OcupationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_vacancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_4erp_vacancy_4erp_ocupation_OcupationId",
                        column: x => x.OcupationId,
                        principalTable: "4erp_ocupation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_4erp_vacancy_4erp_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "4erp_person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_4erp_vacancy_4erp_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "4erp_status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "4erp_candidature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VacancyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_candidature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_4erp_candidature_4erp_person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "4erp_person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_4erp_candidature_4erp_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "4erp_status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_4erp_candidature_4erp_vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "4erp_vacancy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "4erp_vacancy_skill",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VacancyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_4erp_vacancy_skill", x => new { x.SkillId, x.VacancyId });
                    table.ForeignKey(
                        name: "FK_4erp_vacancy_skill_4erp_skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "4erp_skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_4erp_vacancy_skill_4erp_vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "4erp_vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "4erp_bio",
                columns: new[] { "Id", "About", "EducationId", "ExperienceId", "Linkedin", "Resume", "WebSite" },
                values: new object[,]
                {
                    { new Guid("7f9c8d7f-5264-45a7-894d-889a25e2d069"), "Olá, me Chamo Alice, tenho 22 anos, sou entusiasta em tecnologia, apaixonado por computação e também em escovar bit. Minha carreira na área de TI começou ainda novo, com 13 anos descobri o mundo da computação e como ela poderia transformar meu futuro, de forma incansável, inapelável, incansável busquei aprender diveras áreas como infraestrutura TI, Programação, Vários níveis da Arquitetura de Computadores e Software. Atualmente me dedico a alimentar comunidades Open desenvolvendo ativos de infraestrutura e desenvolvendo startups.", null, null, "http://linkedin.com/gabriel-lima", "Trabalhei em grandes empresas como, Invent Software, Trinus Bank, BlueTech, Accenture, Sensedia, Banco BV, Banco Ouribank, Burgerking, Starbucks", "http://linkedin.com/gabriel-lima" },
                    { new Guid("91597911-94ee-42ef-b1c4-c0ce0e9fe883"), "A Infinity é uma empresa dedicada à excelência na construção de softwares e na engenharia de soluções tecnológicas. Com uma abordagem inovadora e personalizada, a Infinity alia expertise técnica e visão estratégica para transformar desafios complexos em oportunidades de crescimento para seus clientes. Nossa missão é desenvolver sistemas que impulsionem a eficiência operacional e a competitividade, adotando metodologias ágeis e tecnologias de ponta para criar soluções robustas e escaláveis. Valorizamos a criatividade, o comprometimento e a busca constante pela melhoria contínua, pilares que nos permitem entregar projetos com alta qualidade e impacto no mercado. Na Infinity, acreditamos que a transformação digital é essencial para a evolução dos negócios, e por isso, investimos continuamente na capacitação de nossos profissionais e na atualização de nossas práticas. Nosso time multidisciplinar trabalha em sinergia, integrando conhecimento técnico e visão de negócio para oferecer soluções completas que atendem às demandas específicas de cada projeto. Através de uma gestão transparente e orientada por resultados, a Infinity consolida sua posição como referência no setor, sempre focada em promover a inovação e contribuir para o sucesso dos nossos parceiros e clientes. Com compromisso e dedicação, seguimos construindo o futuro da tecnologia, transformando ideias em soluções que impulsionam o progresso.", null, null, "http://linkedin.com/infinity", "A Infinity é uma empresa dedicada à excelência na construção de softwares e na engenharia de soluções tecnológicas. Com uma abordagem inovadora e personalizada, a Infinity alia expertise técnica e visão estratégica para transformar desafios complexos em oportunidades de crescimento para seus clientes. Nossa missão é desenvolver sistemas que impulsionem a eficiência operacional e a competitividade, adotando metodologias ágeis e tecnologias de ponta para criar soluções robustas e escaláveis. Valorizamos a criatividade, o comprometimento e a busca constante pela melhoria contínua, pilares que nos permitem entregar projetos com alta qualidade e impacto no mercado. Na Infinity, acreditamos que a transformação digital é essencial para a evolução dos negócios, e por isso, investimos continuamente na capacitação de nossos profissionais e na atualização de nossas práticas. Nosso time multidisciplinar trabalha em sinergia, integrando conhecimento técnico e visão de negócio para oferecer soluções completas que atendem às demandas específicas de cada projeto. Através de uma gestão transparente e orientada por resultados, a Infinity consolida sua posição como referência no setor, sempre focada em promover a inovação e contribuir para o sucesso dos nossos parceiros e clientes. Com compromisso e dedicação, seguimos construindo o futuro da tecnologia, transformando ideias em soluções que impulsionam o progresso.", "http://linkedin.com/infinity" },
                    { new Guid("9b8717ca-9565-4c6d-951f-64e5e9df0ad5"), "Olá, me Chamo Gabriel, tenho 33 anos, sou entusiasta em tecnologia, apaixonado por computação e também em escovar bit. Minha carreira na área de TI começou ainda novo, com 13 anos descobri o mundo da computação e como ela poderia transformar meu futuro, de forma incansável, inapelável, incansável busquei aprender diveras áreas como infraestrutura TI, Programação, Vários níveis da Arquitetura de Computadores e Software. Atualmente me dedico a alimentar comunidades Open desenvolvendo ativos de infraestrutura e desenvolvendo startups.", null, null, "http://linkedin.com/gabriel-lima", "Trabalhei em grandes empresas como, Invent Software, Trinus Bank, BlueTech, Accenture, Sensedia, Banco BV, Banco Ouribank, Burgerking, Starbucks", "http://linkedin.com/gabriel-lima" }
                });

            migrationBuilder.InsertData(
                table: "4erp_ocupation",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("05752402-43cf-42bf-aa56-a553d055ce01"), "Arquiteto de Software", "Arquiteto de Software" },
                    { new Guid("26af6758-9513-4f84-b9b2-d3a421d56996"), "Engenheiro de Software", "Engenheiro de Software" },
                    { new Guid("3fdaa34f-67e4-4325-b9ba-d0ea9a351579"), "Analista de Sistemas", "Analista de Sistemas" },
                    { new Guid("bad88f10-d1e5-4807-9f2c-852e25276b38"), "QA", "QA" },
                    { new Guid("bef71141-4859-46cb-9880-7a83d1532d14"), "Desenvolvedor de Software", "Desenvolvedor de Software" },
                    { new Guid("f0173a5a-473f-4e86-915b-8bd63ed68903"), "Líder Técnico", "Líder Técnico" }
                });

            migrationBuilder.InsertData(
                table: "4erp_person",
                columns: new[] { "Id", "BioId", "FantasyName", "FirstName", "LastName", "LegalName", "PhoneId", "TaxId", "Type" },
                values: new object[] { new Guid("ebab34b5-3543-4c53-a3cc-03bf86567081"), null, "4ERP", "", "", "4ERP", null, "18120830000119", -1 });

            migrationBuilder.InsertData(
                table: "4erp_phone",
                columns: new[] { "Id", "DDD", "DDI", "Number" },
                values: new object[,]
                {
                    { new Guid("507accd1-020a-40fe-89c1-73d329133d2a"), "62", "+55", "984887715" },
                    { new Guid("5a44e663-ef90-4fbb-a61e-0633b1dae5f9"), "62", "+55", "984887715" },
                    { new Guid("c1808532-d4d3-4429-bb35-69b4c4f50c9d"), "62", "+55", "984887715" }
                });

            migrationBuilder.InsertData(
                table: "4erp_role",
                columns: new[] { "Id", "Alias", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b"), "administrator:company:system:*", "Administrador do sistema", "Company" },
                    { new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16"), "administrator:person:system:*", "Usuario do sistema", "Person" },
                    { new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c"), "administrator:system:*", "Administrador do sistema", "Administrador" }
                });

            migrationBuilder.InsertData(
                table: "4erp_skill",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1374000b-ddc6-4668-8e0c-f75f302106a2"), "Linguagem moderna desenvolvida pelo Google, focada em performance e concorrência.", "Go" },
                    { new Guid("3b0c221a-9de4-4624-9a7b-33c6705a644e"), "Linguagem segura e eficiente, utilizada para sistemas embarcados, segurança e alto desempenho.", "Rust" },
                    { new Guid("48aa83d8-bbdf-45c5-8909-0155d254e5f4"), "Linguagem de programação essencial para desenvolvimento web, utilizada no frontend e backend.", "Typescript" },
                    { new Guid("49dae8b3-25b0-4264-a4f2-0df5932101bd"), "Linguagem versátil usada para desenvolvimento web, automação, ciência de dados e inteligência artificial.", "Python" },
                    { new Guid("4b867bb5-eb6b-41cc-87a6-5041927be496"), "Linguagem de programação orientada a objetos utilizada para desenvolvimento web, desktop e mobile.", "C#" },
                    { new Guid("718e2ce0-a235-432f-bd9f-9dbe272d5924"), "Linguagem popular para desenvolvimento corporativo, aplicações móveis (Android) e sistemas distribuídos.", "Java" }
                });

            migrationBuilder.InsertData(
                table: "4erp_status",
                columns: new[] { "Id", "Description", "Name", "Slug" },
                values: new object[,]
                {
                    { new Guid("0558afb1-4a6e-46fb-bd0d-2d14b756b348"), "Rejeitado", "rejected", "candidature_status" },
                    { new Guid("29d6e75a-3808-482d-9316-493a73dc6fe0"), "Avaliando candidato", "candidate_evaluation", "candidature_status" },
                    { new Guid("44147d27-ff38-413f-8187-cce3e5d603f4"), "Avaliação técnica", "candidate_evaluation", "candidature_status" },
                    { new Guid("5eda1e3c-1b97-480c-bee4-9ea0c61859c5"), "Aguardando análise", "waiting", "candidature_status" },
                    { new Guid("711df75d-cebf-46e9-8873-6a8ce6670b5b"), "Entrevista", "interview", "candidature_status" },
                    { new Guid("735b983b-db8f-4ece-a96c-6007fb1de5b9"), "Aprovado", "approved", "candidature_status" },
                    { new Guid("b1cd5678-0f19-4b2f-87ff-f1a0d75421f2"), "Em andamento", "vacancy_in_progress", "vacancy_status" },
                    { new Guid("bb0bc2c3-62c4-4159-a680-5a45226ab7b1"), "Em breve", "vacancy_waiting", "vacancy_status" },
                    { new Guid("e04f8400-f060-4c04-8e75-eb540cc3baba"), "Cancelada", "vacancy_canceled", "vacancy_status" },
                    { new Guid("e795eaaf-e08c-430e-a652-242a8dcf886d"), "Finalizada", "vacancy_done", "vacancy_status" }
                });

            migrationBuilder.InsertData(
                table: "4erp_experience",
                columns: new[] { "Id", "BioId", "Current", "DateEnd", "DateInit", "Description", "Name" },
                values: new object[] { new Guid("4eb08c7e-e56b-49ff-b074-8e863b083738"), new Guid("9b8717ca-9565-4c6d-951f-64e5e9df0ad5"), false, null, null, "Atuei como desenvoledor por 2 anos, onde pude contribuir na construção de aplicações em SAPUI5.", "Invent Software" });

            migrationBuilder.InsertData(
                table: "4erp_person",
                columns: new[] { "Id", "BioId", "FantasyName", "FirstName", "LastName", "LegalName", "PhoneId", "TaxId", "Type" },
                values: new object[,]
                {
                    { new Guid("05c80ccd-8c9a-4aa2-9dd5-b80e2e30030f"), new Guid("7f9c8d7f-5264-45a7-894d-889a25e2d069"), "", "Alice", "Borges Azevedo Lima", "", new Guid("c1808532-d4d3-4429-bb35-69b4c4f50c9d"), "03931830179", 1 },
                    { new Guid("438d2927-d1d6-442d-97b7-d4d41a67fc35"), new Guid("9b8717ca-9565-4c6d-951f-64e5e9df0ad5"), "", "Gabriel", "Borges Da Silva", "", new Guid("5a44e663-ef90-4fbb-a61e-0633b1dae5f9"), "03931830179", 1 },
                    { new Guid("b3ff401f-0c41-47de-a3c8-0d2c5b65e787"), new Guid("91597911-94ee-42ef-b1c4-c0ce0e9fe883"), "Infinity", "", "", "Infinity", new Guid("507accd1-020a-40fe-89c1-73d329133d2a"), "18120830000118", 0 }
                });

            migrationBuilder.InsertData(
                table: "4erp_scope",
                columns: new[] { "Id", "Alias", "Description", "Name", "RoleId" },
                values: new object[,]
                {
                    { new Guid("2025e44f-a232-43a5-9b8c-769fbd18a06f"), "administrator:vacancy:create", "Pode criar vagas no sistema", "Criador de vagas", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("3f87c5ea-edc8-4826-b67b-396a0d134bef"), "administrator:company:account:edit", "Pode editar suas configurações de conta", "Pode editar suas configurações de conta", new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b") },
                    { new Guid("44a3dc2f-1810-4957-8bd3-0e526ee8d44f"), "administrator:person:vacancy:view", "Pode criar vagas no sistema", "Pode ver vagas disponíveis", new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16") },
                    { new Guid("4852b3ab-dd40-47a6-9548-643ae0f6967e"), "administrator:vacancy:read", "Pode ver vagas no sistema", "Leitor de vagas", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("4a5a2b1d-f0e2-493d-8231-e975a295006b"), "administrator:user:create", "Pode criar uma nova empresa no sistema", "Pode criar uma nova empresa", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("6b7fc3b4-6c16-41ee-a6ab-0c7dfd21d41e"), "administrator:company:vacancy:create", "Pode criar vagas no sistema", "Criador de vagas", new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b") },
                    { new Guid("72bdf067-cc4b-4e36-983b-cb1e7284af48"), "administrator:vacancy:remove", "Pode remover vagas no sistema", "Removedor de vagas", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("8cb9c7e0-ba1a-452a-b0b3-49cc930a049c"), "administrator:company:editor", "Pode editar uma nova empresa no sistema", "Pode editar uma nova empresa", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("98593af7-4cf6-4cfd-8947-bc6cfae07229"), "administrator:account:edit", "Pode editar suas configurações de conta", "Pode editar suas configurações de conta", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("9d631e92-5e18-40c5-bb62-a76ef782a77e"), "administrator:person:candidate:create", "Pode editar vagas no sistema", "Pode se candidatar a uma vaga", new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16") },
                    { new Guid("a490ea3e-ce26-4d2d-b2a2-77a28cc824cb"), "administrator:company:vacancy:editor", "Pode editar vagas no sistema", "Editor de vagas", new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b") },
                    { new Guid("a77e474c-6047-4914-941d-29babbbe07b0"), "administrator:person:account:edit", "Pode editar suas configurações de conta", "Pode editar suas configurações de conta", new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16") },
                    { new Guid("b2d5653f-4e4c-4ec1-94f9-bd458b72d27a"), "administrator:vacancy:editor", "Pode editar vagas no sistema", "Editor de vagas", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("b2d9a2bf-62e1-4ce2-b5a5-2c9d6002d01c"), "administrator:user:read", "Pode ler usuários no sistema", "Pode ler usuários", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("b4eff319-159c-4cc9-bcb1-0b36c574900f"), "administrator:user:editor", "Pode editar um novo usuários", "Pode editar um novo usuários", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("b74b8e82-0ba6-4c36-a34d-7076862c8882"), "administrator:person:candidate:read", "Pode ver vagas no sistema", "Pode ver candidaturas feitas a vagas", new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16") },
                    { new Guid("bdefc049-c893-4871-90e4-5a69298b5e05"), "administrator:company:remove", "Pode remover empresas no sistema", "Pode remover empresas", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("c5878d7a-c03a-4a88-a952-68d7e1d07b0a"), "administrator:company:vacancy:read", "Pode ver vagas no sistema", "Leitor de vagas", new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b") },
                    { new Guid("cac88890-5651-4548-8bc8-71683a9dac7e"), "administrator:company:read", "Pode ler empresas no sistema", "Pode ler empresas", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("e9c764a4-5139-43a1-8d20-a613fe5bf9d3"), "administrator:company:vacancy:remove", "Pode remover vagas no sistema", "Removedor de vagas", new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b") },
                    { new Guid("ef924721-b753-4997-b94f-74ea97e3a3d3"), "administrator:company:create", "Pode criar uma nova empresa", "Pode criar uma nova empresa", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("f48f60d8-ea44-4616-beb2-c031aca31a3d"), "administrator:user:remove", "Pode remover uma nova empresa no sistema", "Pode remover empresas", new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c") },
                    { new Guid("f7bf4f05-4268-428a-b225-1a67e2bf06af"), "administrator:person:candidate:remove", "Pode remover vagas no sistema", "Pode remover sua candidatura de uma vaga", new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16") }
                });

            migrationBuilder.InsertData(
                table: "4erp_user",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "PersonId", "RoleId", "UpdatedAt", "deletedAt" },
                values: new object[,]
                {
                    { new Guid("36374eca-40aa-4a2f-9310-4ba40580bc21"), null, "administrator@4erp.io", "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu", new Guid("ebab34b5-3543-4c53-a3cc-03bf86567081"), new Guid("ec9f1294-3ac6-4496-9ace-13a10056633c"), null, null },
                    { new Guid("48a29331-9c60-42a9-ad70-e69fc6bc83c2"), null, "gabriel@4erp.io", "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu", new Guid("438d2927-d1d6-442d-97b7-d4d41a67fc35"), new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16"), null, null },
                    { new Guid("812fefd5-98c7-4257-a609-3eeeb0ac549d"), null, "alice@4erp.io", "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu", new Guid("05c80ccd-8c9a-4aa2-9dd5-b80e2e30030f"), new Guid("786bc7e7-8365-4d08-90fb-7fc26d284d16"), null, null },
                    { new Guid("e2920e37-1fb6-4f23-960e-2ce8a1017c09"), null, "infinity@4erp.io", "$2a$11$z60nhm6q5Vonv5b6/97DAuI8G.TcxwDgyAgd21d.AiTHgIjWxJgRu", new Guid("b3ff401f-0c41-47de-a3c8-0d2c5b65e787"), new Guid("6f8a65c6-fc8f-439f-bb92-28c2253c000b"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_4erp_bio_EducationId",
                table: "4erp_bio",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_bio_ExperienceId",
                table: "4erp_bio",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_candidature_PersonId",
                table: "4erp_candidature",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_candidature_StatusId",
                table: "4erp_candidature",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_candidature_VacancyId",
                table: "4erp_candidature",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_education_BioId",
                table: "4erp_education",
                column: "BioId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_experience_BioId",
                table: "4erp_experience",
                column: "BioId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_person_BioId",
                table: "4erp_person",
                column: "BioId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_person_PhoneId",
                table: "4erp_person",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_person_skill_SkillId",
                table: "4erp_person_skill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_scope_RoleId",
                table: "4erp_scope",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_user_PersonId",
                table: "4erp_user",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_user_RoleId",
                table: "4erp_user",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_vacancy_OcupationId",
                table: "4erp_vacancy",
                column: "OcupationId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_vacancy_PersonId",
                table: "4erp_vacancy",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_vacancy_StatusId",
                table: "4erp_vacancy",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_4erp_vacancy_skill_VacancyId",
                table: "4erp_vacancy_skill",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_4erp_bio_4erp_education_EducationId",
                table: "4erp_bio",
                column: "EducationId",
                principalTable: "4erp_education",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_4erp_bio_4erp_experience_ExperienceId",
                table: "4erp_bio",
                column: "ExperienceId",
                principalTable: "4erp_experience",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_4erp_bio_4erp_education_EducationId",
                table: "4erp_bio");

            migrationBuilder.DropForeignKey(
                name: "FK_4erp_bio_4erp_experience_ExperienceId",
                table: "4erp_bio");

            migrationBuilder.DropTable(
                name: "4erp_candidature");

            migrationBuilder.DropTable(
                name: "4erp_person_skill");

            migrationBuilder.DropTable(
                name: "4erp_scope");

            migrationBuilder.DropTable(
                name: "4erp_user");

            migrationBuilder.DropTable(
                name: "4erp_vacancy_skill");

            migrationBuilder.DropTable(
                name: "4erp_role");

            migrationBuilder.DropTable(
                name: "4erp_skill");

            migrationBuilder.DropTable(
                name: "4erp_vacancy");

            migrationBuilder.DropTable(
                name: "4erp_ocupation");

            migrationBuilder.DropTable(
                name: "4erp_person");

            migrationBuilder.DropTable(
                name: "4erp_status");

            migrationBuilder.DropTable(
                name: "4erp_phone");

            migrationBuilder.DropTable(
                name: "4erp_education");

            migrationBuilder.DropTable(
                name: "4erp_experience");

            migrationBuilder.DropTable(
                name: "4erp_bio");
        }
    }
}
