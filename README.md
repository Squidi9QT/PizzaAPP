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
| `Interfaces.cs` | интерфейс `IPriceable` для всего, у чего есть цена |
| `PizzaBase.cs`, `Crust.cs`, `Ingredient.cs` | основа, корж и ингредиенты, общий предок `BaseProduct` |
| `Pizza.cs` | пицца и её размер `PizzaSize` |
| `Order.cs`, `OrderTimeComparer.cs` | заказ и сортировка заказов по времени |
| `Sclad.cs` | склад |
| `Admin.cs` | действия администратора |
| `KurbanСhef.cs` | точка входа, консольное меню |
