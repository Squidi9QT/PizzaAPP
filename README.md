# PizzaAPP

Учебная лаба с 1 курса: консольная пиццерия на C#. Можно собрать пиццу из основы, коржа и ингредиентов, оформить заказ и посмотреть склад.

## Как запустить

Нужен [.NET 9 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet run
```

## Что внутри

| Файл | Что там |
|---|---|
| `Interfaces.cs` | интерфейс `IPriceable` для всего, у чего есть цена, и общий предок `BaseProduct` |
| `PizzaBase.cs`, `Ingredient.cs` | основа и ингредиенты, наследуются от `BaseProduct` |
| `Crust.cs` | корж |
| `Pizza.cs` | пицца и её размер `PizzaSize` |
| `Order.cs`, `OrderTimeComparer.cs` | заказ и сортировка заказов по времени |
| `Sclad.cs` | стартовые данные: ингредиенты, основы и пиццы |
| `Admin.cs` | меню администратора |
| `KurbanСhef.cs` | точка входа (`Main`), консольное меню |
