# 🌌 SpaceAtlas
RESTful API сервис для ведения каталога космических объектов (звёзд и планет) с ролевым доступом, фильтрацией и возможностью прикрепления медиаданных.
---
## 🛠 Стек технологий
- **Платформа:** .NET 8 (C# 12) / ASP.NET Core Web API
- **База данных и ORM:** PostgreSQL, Entity Framework Core 9 (Npgsql), EF Core Migrations
- **Аутентификация и безопасность:** ASP.NET Core Identity (GUID keys), JWT Bearer Token
- **Валидация:** FluentValidation
- **Маппинг данных:** AutoMapper
- **Логирование:** Serilog (с обогащением CorrelationId)
- **Документация:** Swagger / OpenAPI (Swashbuckle)
---
## 🏛 Архитектура проекта
Проект разделен на три независимых слоя:
1. **`SpaceAtlas.Service`** (Слой представления / API):
   - Контроллеры API (`AuthController`, `PlanetController`, `StarController`, `UserController`).
   - DTO-модели запросов и ответов (`Request` / `Response`).
   - Валидаторы входных данных (`FluentValidation`).
2. **`SpaceAtlas.BL`** (Слой бизнес-логики):
   - Доменные сервисы (`PlanetService`, `StarService`, `UserService`).
   - Бизнес-модели и фильтры (`PlanetModel`, `StarModel`, `UserModel`).
   - Профили маппинга AutoMapper.
   - Пользовательские исключения (например, `IncorrectAuthorException`).
3. **`SpaceAtlas.DataAccess`** (Слой доступа к данным):
   - Контекст базы данных `SpaceAtlasDbContext`.
   - Обобщенный репозиторий `IRepository<T>` / `Repository<T>`.
   - Сущности БД: `UserEntity`, `StarEntity`, `PlanetEntity`.
   - Миграции базы данных.
