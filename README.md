# TestWebAPI
Проект Web API, реализованный средствами ASP.NET Core, EntityFramework
## Описание предметной области
+ В онлайн-сервисе обучения иностранному языку пользователи изучают иностранные слова путём выполнения различных заданий.
+ У каждого пользователя есть адрес электронной почты, логин пароль, имя и дата регистрации (задаётся автоматически при добавлении пользователя).
+ Каждое задание в сервисе характеризуется названием, описанием, типом (строка, enum или отдельная таблица, на выбор) и уровнем сложности (дробное число). 
+ Каждый пользователь может выполнять множество заданий, задания могут выполняться множеством пользователей. О факте прохождения каждого задания также сохраняется дата.
+ В каждое задание входит множество слов, а каждое слово может входить во множество заданий.
+ Каждое слово характеризуется текстом на иностранном языке, переводом на текущем языке и путём до изображения, характеризующего его.
+ Пользователь может изучить множество слов, а каждое слово может быть изучено множеством пользователей. Причём каждое слово пользователь может изучить на определённый процент (число, от 0 до 100) после выполнения заданий.

### Ef-диаграмма
На основе описания предметной области была спроектирована ef-диаграмма:
![ef_diagram](./TestWebAPI/Files/Images/Ef_diagram.jpg)

## Database
В проекте были созданы модели сущностей БД, произведены миграции в SQL Server Management Studio. Выполнено наполнение БД тестовыми псевдо-реалистичными данными путём их импорта из файлов CSV посредством запросов SQl.
### Модели сущностей БД
Модель сущности Users:
```C#
public class Users
{
    [Key]
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public DateTime DateReg { get; set; }
        
    public virtual ICollection<CompletedTasks> CompletedTasks { get; set; }
    public virtual ICollection<LearnedWords> LearnedWords { get; set; }
}
```
Модель сущности Tasks:
```C#
public class Tasks
{
    [Key]
    public int TaskId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double DifficaltyLvl { get; set; }
    [ForeignKey(nameof(Types))]
    public int TypeId { get; set; }
        
    public virtual Types Types { get; set; }
    public virtual ICollection<CompletedTasks> CompletedTasks { get; set; }
    public virtual ICollection<WordsTasks> WordsTasks { get; set; }
}
```
Модель сущности Types:
```C#
public class Types
{
    [Key]
    public int TypeId { get; set; }
    public string Name { get; set; }
        
    public virtual ICollection<Tasks> Tasks { get; set; }
}
```
Модель сущности Words:
```C#
public class Words
{
    [Key]
    public int WordId { get; set; }
    public string Word { get; set; }
    public string Translation { get; set; }
    public string ImagePath { get; set; }
        
    public virtual ICollection<WordsTasks> WordsTasks { get; set; }
    public virtual ICollection<LearnedWords> LearnedWords { get; set; }
}
```
Модель сущности WordsTasks:
```C#
public class WordsTasks
{
    [ForeignKey(nameof(Tasks))]
    public int TaskId { get; set; }
        
    [ForeignKey(nameof(Words))]
    public int WordId { get; set; }

    public virtual Tasks Tasks { get; set; }
    public virtual Words Words { get; set; }
}
```
Модель сущности LearnedWords:
```C#
public class LearnedWords
{
    [ForeignKey(nameof(Users))]
    public int UserId { get; set; }
        
    [ForeignKey(nameof(Words))]
    public int WordId { get; set; }
        
    public int Percent { get; set; }

    public virtual Users Users { get; set; }
    public virtual Words Words { get; set; }
}
```
Модель сущности CompletedTasks:
```C#
public class CompletedTasks
{
    [ForeignKey(nameof(Users))]
    public int UserId { get; set; }
        
    [ForeignKey(nameof(Tasks))]
    public int TaskId { get; set; }
        
    public DateTime DateCompletion { get; set; }
        
    public virtual Users Users { get; set; }
    public virtual Tasks Tasks { get; set; }
}
```

### Миграции
В проекте было реализовано несколько миграций, их историю можно посмотреть в файлах папки Migrations.
В ходе миграций была создана локальная БД SQl Server со структурой:









