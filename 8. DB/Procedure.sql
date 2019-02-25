/* 13	Разработка функций и процедур
13.1	Написать процедуру, которая возвращает самый крупный заказ для каждого из продавцов за определенный год. 
В результатах не может быть несколько заказов одного продавца, должен быть только один и самый крупный. 
В результатах запроса должны быть выведены следующие колонки: колонка с именем и фамилией продавца (FirstName и LastName – пример: Nancy Davolio),
номер заказа и его стоимость. В запросе надо учитывать Discount при продаже товаров. 
Процедуре передается год, за который надо сделать отчет, и количество возвращаемых записей. Результаты запроса должны быть упорядочены по убыванию суммы заказа.
Процедура должна быть реализована с использованием оператора SELECT и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ.
.Название функции соответственно GreatestOrders. Необходимо продемонстрировать использование этих процедур.
Также помимо демонстрации вызовов процедур в скрипте Query.sql надо написать отдельный ДОПОЛНИТЕЛЬНЫЙ проверочный запрос для тестирования правильности работы процедуры GreatestOrders.
Проверочный запрос должен выводить в удобном для сравнения с результатами работы процедур виде для определенного продавца для всех его заказов
за определенный указанный год в результатах следующие колонки: имя продавца, номер заказа, сумму заказа. 
Проверочный запрос не должен повторять запрос, написанный в процедуре, - он должен выполнять только то, что описано в требованиях по нему.
ВСЕ ЗАПРОСЫ ПО ВЫЗОВУ ПРОЦЕДУР ДОЛЖНЫ БЫТЬ НАПИСАНЫ В ФАЙЛЕ Query.sql – см. пояснение ниже в разделе «Требования к оформлению».
*/

CREATE PROCEDURE [dbo].GreatestOrders
	@OrdYear int = '1998',
	@Top int = 10
AS
WITH OrderSummary AS 
(
SELECT O.EmployeeID,
	O.OrderID, 
	SUM(CONVERT(decimal(14,2), OD.Quantity * (1-OD.Discount) * OD.UnitPrice)) Totals
FROM [Northwind].Orders AS O
LEFT JOIN [Northwind].[Order Details] AS OD
ON O.OrderID = OD.OrderID 
WHERE 
	DATEPART(yyyy,O.OrderDate) = @OrdYear
GROUP BY O.EmployeeID, O.OrderID
)

SELECT TOP (@Top)
	(SELECT Employees.FirstName+ ' '+ Employees.LastName 
	FROM Northwind.Employees
	WHERE Employees.EmployeeID = O1.EmployeeID ) Seller,
	MAX(O2.OrderID) OrderID, 
	MAX(O1.Totals) Totals
FROM OrderSummary AS O1
INNER JOIN OrderSummary AS O2
ON O1.EmployeeID=O2.EmployeeID
	AND O1.Totals=O2.Totals
GROUP BY O1.EmployeeID
ORDER BY Totals DESC

GO

/*13.2	Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку доставки в днях (разница между OrderDate и ShippedDate).  
В результатах должны быть возвращены заказы, срок которых превышает переданное значение или еще недоставленные заказы. 
Значению по умолчанию для передаваемого срока 35 дней. Название процедуры ShippedOrdersDiff.
Процедура должна высвечивать следующие колонки: OrderID, OrderDate, ShippedDate, ShippedDelay (разность в днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение). 
Необходимо продемонстрировать использование этой процедуры.
*/

CREATE PROCEDURE [dbo].ShippedOrdersDiff
	@dayShipped int = '35'
AS

SELECT 
	O.OrderID, 
	O.OrderDate, 
	O.ShippedDate, 
	DATEDIFF(day, O.OrderDate, O.ShippedDate) AS ShippedDelay,
	@dayShipped AS SpecifiedDelay
FROM [Northwind].Orders AS O
WHERE 
	O.ShippedDate IS NULL 
	OR DATEDIFF(day, O.OrderDate, O.ShippedDate) > @dayShipped

GO
/*13.3	 Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, так и подчиненных его подчиненных. 
В качестве входного параметра функции используется EmployeeID. Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать оператор PRINT) согласно иерархии подчинения.
Продавец, для которого надо найти подчиненных также должен быть высвечен. Название процедуры SubordinationInfo. 
В качестве алгоритма для решения этой задачи надо использовать пример, приведенный в Books Online и рекомендованный Microsoft для решения подобного типа задач. 
Продемонстрировать использование процедуры.
*/

CREATE PROCEDURE [dbo].SubordinationInfo
	@EmployeeID int = 2
AS
DECLARE  @SubordinationНierarchy VARCHAR(max) = '';

WITH EmpsRN AS --нумеруем строки внутри узла дерева. Порядок строк устанавливается здесь:  ORDER BY e.EmployeeID
(
	SELECT e.ReportsTo, e.EmployeeID, e.FirstName, e.LastName,
	ROW_NUMBER() OVER(PARTITION BY e.ReportsTo ORDER BY e.EmployeeID) AS n
	FROM Northwind.Employees AS e
)
,
DirectReports (ManagerID, EmployeeID, Title, Level, OrderPath)
AS
(
	SELECT e.ReportsTo, e.EmployeeID, e.FirstName+' '+e.LastName, 
		0 AS Level, 
		CAST(0x AS VARBINARY(MAX)) AS OrderPath
	FROM Northwind.Employees AS e
	WHERE e.EmployeeID = @EmployeeID
    
	UNION ALL

	SELECT e.ReportsTo, e.EmployeeID,  e.FirstName+' '+e.LastName, 
		Level + 1,  
		d.OrderPath+ CAST(n AS BINARY(2))
	FROM EmpsRN AS e
	INNER JOIN DirectReports AS d
		ON e.ReportsTo = d.EmployeeID
)

SELECT @SubordinationНierarchy = @SubordinationНierarchy  + CHAR(13)  + REPLICATE(' | ', Level) + Title 
FROM DirectReports ORDER BY OrderPath;

PRINT 'Subordination hierarchy: '
PRINT @SubordinationНierarchy

GO
/*13.4	 Написать функцию, которая определяет, есть ли у продавца подчиненные. Возвращает тип данных BIT. В качестве входного параметра функции используется EmployeeID. 
Название функции IsBoss. Продемонстрировать использование функции для всех продавцов из таблицы Employees.
*/

CREATE FUNCTION [dbo].IsBoss (@EmployeeID int)
RETURNS bit
AS
BEGIN 
	DECLARE @result bit
	
	IF EXISTS
		(SELECT ReportsTo FROM Northwind.Employees AS E
		WHERE ReportsTo = @EmployeeID)
		SET @result = 1
	ELSE 
		SET @result = 0

	RETURN (@result)

END

GO
