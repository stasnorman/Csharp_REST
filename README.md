# API для мастер-класса от Института перспективных технологий (МИРЭА - РТУ)
Репозитории мастер-класса:
- [Open-Door-Backend](https://github.com/stasnorman/Open-Door-Backend)
- [Csharp_REST](https://github.com/stasnorman/Csharp_REST)
- [PHPApi-easy-and-fast](https://github.com/stasnorman/PHPApi-easy-and-fast)


## Описание проекта

Это REST API для управления интернет-магазином комиксов. API предоставляет функциональность для создания, редактирования, удаления и получения данных о комиксах, а также документацию с помощью Swagger.

## Технологии и зависимости

- **ASP.NET Core 8.0**: основная технология для построения API
- **MySQL**: реляционная база данных для хранения данных о комиксах
- **Entity Framework Core**: ORM для работы с базой данных
- **Pomelo.EntityFrameworkCore.MySql**: провайдер для подключения к MySQL
- **Swagger/OpenAPI**: для генерации документации API и взаимодействия с ним

## Установка и настройка

### 1. Установка зависимостей

Убедитесь, что у вас установлены следующие компоненты:

- .NET SDK 8.0
- MySQL (для базы данных)
- Visual Studio 2022 (или любой другой редактор, поддерживающий ASP.NET Core)

### 2. Клонирование репозитория

```bash
git clone https://github.com/your-username/OpenDoor.git
cd OpenDoor
```

### 3. Настройка базы данных

#### Подключение к MySQL

Откройте файл `appsettings.json` и измените строку подключения к базе данных на ваши реальные данные:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=mysql.burnfeniks.myjino.ru;Database=burnfeniks_opendoor;User=your_mysql_user;Password=your_mysql_password;"
}
```

#### Миграции базы данных

Для создания и обновления базы данных выполните следующие команды в терминале:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Эти команды создадут необходимые таблицы в базе данных.

### 4. Запуск приложения

После успешного обновления базы данных запустите приложение:

```bash
dotnet run
```

API будет доступно по адресу `http://localhost:5000`.

### 5. Swagger UI

Swagger UI позволяет удобно просматривать и тестировать API. Swagger-документация доступна по следующему URL (локально):

```bash
http://localhost:5000/swagger/index.html
```

## Эндпоинты API

| Метод   | Маршрут              | Описание                                |
|---------|----------------------|-----------------------------------------|
| `GET`   | `/comics`            | Получить список всех комиксов           |
| `GET`   | `/comics/{id}`        | Получить информацию о комиксе по ID     |
| `POST`  | `/comics`            | Добавить новый комикс                   |
| `PUT`   | `/comics/{id}`        | Полностью обновить информацию о комиксе |
| `PATCH` | `/comics/{id}`        | Частично обновить информацию о комиксе  |
| `DELETE`| `/comics/{id}`        | Удалить комикс по ID                    |

## Логика и структура проекта

### Основные файлы и директории:

- `Program.cs` — точка входа в приложение.
- `Models/` — содержит модели базы данных.
- `Services/` — содержит сервисы, которые инкапсулируют бизнес-логику API.
- `Controllers/` — содержит контроллеры, которые обрабатывают запросы к API.
- `appsettings.json` — содержит конфигурации приложения, включая строку подключения к базе данных.

## Документация API

Документация по API автоматически генерируется Swagger и доступна по указанному выше URL.

### Конфигурация Swagger

Swagger был добавлен в проект через файл `Program.cs`:

```csharp
builder.Services.AddSwaggerGen();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenDoor API v1");
        c.RoutePrefix = string.Empty;
    });
}
```

## Лицензия

Этот проект лицензируется по лицензии MIT.
