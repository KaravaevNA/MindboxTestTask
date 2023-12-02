SELECT Products.Id, Products.Name AS ProductName, Categories.Name AS CategoryName
FROM Products
LEFT JOIN CategoryProduct ON Products.Id = CategoryProduct.ProductId
LEFT JOIN Categories ON CategoryProduct.CategoryId = Categories.Id;