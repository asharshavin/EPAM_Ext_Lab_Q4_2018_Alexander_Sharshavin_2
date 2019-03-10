
SET SHOWPLAN_TEXT OFF
GO

USE Northwind
GO 

/* Achtung! Перед использованием процедур сделать Publish !!! */

--[ki. Обязательным требованием к написанию запросов является всегда указывать схему. Это увеличивает производительность, это обеспечивает сохранение планнов выполнения запросов:
-- The reason is simple: SQL Server does cache the query plan for ad-hoc queries, but if the schema name isn’t present in the query the cache can’t be re-used for other users, only for the same user.]

/*1	Работа с типами данных Date, NULL значениями, трехзначная логика. 
Возвращение определенных значений в результатах запроса в зависимости от полученных первоначальных значений результата запроса. 
Высветка в результатах запроса только определенных колонок.

[ki. Not Accepted] 
1.1	Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно и которые доставлены с ShipVia >= 2. 
Формат указания даты должен быть верным при любых региональных настройках,
согласно требованиям статьи “Writing International Transact-SQL Statements” в Books Online раздел “Accessing and Changing Relational Data Overview”.
Этот метод использовать далее для всех заданий. Запрос должен высвечивать только колонки OrderID, ShippedDate и ShipVia. 
Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate. 

Ответ: Строки с ShippedDate со значением NULL в результат запроса не попали, т.к. любое сравнение с NULL есть NULL, т.е. не истина. 
Чтобы показывались эти строки заменить на WHERE (T.ShippedDate>=@date OR T.ShippedDate IS NULL)AND T.ShipVia>=2
*/
-- [ki.] Это условие не выполненно. Формат указания даты должен быть верным при любых региональных настройках, согласно требованиям статьи “Writing International Transact-SQL Statements” в Books Online раздел “Accessing and Changing Relational Data Overview”.
-- Вот как должно быть CONVERT(DATETIME, '19980506', 101)  

DECLARE @date date= '1998-05-06';  

SELECT T.OrderID, T.ShippedDate, T.ShipVia
FROM Northwind.dbo.Orders AS T 
WHERE T.ShippedDate>=@date AND T.ShipVia>=2

--[ki. Accepted]
/*1.2	Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ – использовать системную функцию CASЕ. 
Запрос должен высвечивать только колонки OrderID и ShippedDate.
*/
SELECT T.OrderID, 
	CASE 
		WHEN T.ShippedDate IS NULL THEN 'Not shipped'
	END AS ShippedState
FROM Northwind.dbo.Orders AS T 
WHERE T.ShippedDate IS NULL

--[ki. Accepted]
/*1.3	Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту дату или которые еще не доставлены. 
В запросе должны высвечиваться только колонки OrderID (переименовать в Order Number) и ShippedDate (переименовать в Shipped Date).
В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, 
для остальных значений высвечивать дату в формате по умолчанию.
*/

DECLARE @date date= '1998-05-06';  

SELECT T.OrderID AS OrderNumber, 
	CASE 
		WHEN T.ShippedDate IS NULL THEN 'Not shipped'
		ELSE CAST(T.ShippedDate AS nvarchar(30)) 
	END AS [Shipped Date]
FROM Northwind.dbo.Orders AS T 
WHERE (T.ShippedDate>@date OR T.ShippedDate IS NULL)

--[ki.] Еще бы хорошо писать N'USA'... accepted
/*2	Использование операторов IN, DISTINCT, ORDER BY, NOT
2.1	Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. Запрос сделать с только помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков и по месту проживания.
*/

SELECT T.ContactName, T.Country 
FROM Northwind.dbo.Customers AS T 
WHERE T.Country IN ('USA', 'Canada')
ORDER BY T.ContactName, T.Country 

--[ki. Accepted]
/*2.2	Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. Запрос сделать с помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса по имени заказчиков.
*/

SELECT T.ContactName, T.Country 
FROM Northwind.dbo.Customers AS T 
WHERE T.Country NOT IN (N'USA', N'Canada')
ORDER BY T.ContactName

--[ki. Accepted]
/*2.3	Выбрать из таблицы Customers все страны, в которых проживают заказчики.
Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах запроса. 
*/

SELECT DISTINCT T.Country 
FROM Northwind.dbo.Customers AS T 
ORDER BY T.Country DESC

--[ki. Accepted]
/*3	Использование оператора BETWEEN, DISTINCT
3.1	Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться),
где встречаются продукты с количеством от 3 до 10 включительно – это колонка Quantity в таблице Order Details. 
Использовать оператор BETWEEN. Запрос должен высвечивать только колонку OrderID.
*/

SELECT DISTINCT T.OrderID
FROM Northwind.dbo.[Order Details] AS T 
WHERE T.Quantity BETWEEN 3 AND 10

--[ki. Accepted]
/*3.2	Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g. Использовать оператор BETWEEN. 
Проверить, что в результаты запроса попадает Germany. Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.
*/

--SET SHOWPLAN_TEXT ON
--GO

SELECT T.CustomerID, T.Country 
FROM Northwind.dbo.Customers AS T 
WHERE SUBSTRING(T.Country, 1, 1) BETWEEN 'b' AND 'g'
ORDER BY T.Country 

/*3.3	Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g, не используя оператор BETWEEN. 
С помощью опции “Execution Plan” определить какой запрос предпочтительнее 3.2 или 3.3 
– для этого надо ввести в скрипт выполнение текстового Execution Plan-a для двух этих запросов,
результаты выполнения Execution Plan надо ввести в скрипт в виде комментария и по их результатам дать ответ на вопрос 
– по какому параметру было проведено сравнение. Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.

Ответ: 

Execution plan задачи 3.2
  |--Sort(ORDER BY:([T].[Country] ASC))
       |--Clustered Index Scan(OBJECT:([northwind].[Northwind].[Customers].[PK_Customers] AS [T]), WHERE:(substring([northwind].[Northwind].[Customers].[Country] as [T].[Country],(1),(1))>=N'b' AND substring([northwind].[Northwind].[Customers].[Country] as [T].[Country],(1),(1))<=N'g'))

Execution plan задачи 3.3
  |--Sort(ORDER BY:([T].[Country] ASC))
       |--Clustered Index Scan(OBJECT:([northwind].[Northwind].[Customers].[PK_Customers] AS [T]), WHERE:([northwind].[Northwind].[Customers].[Country] as [T].[Country] like N'[b-g]%'))

1. В обоих вариантах используется Clustered Index Scan (сканирование таблицы), что не эффективно. Сказать какой запрос предпочтительнее, исходя из текстового Execution Plan, я затрудняюсь. 
Используя графический режим Execution Plan, удалось выявить что  Estimated Number of Rows (Оценочное число строк) в задаче 3.2 составил 44 строки, в задаче 3.3 составил 44 строки.  
Вероятно вариант 3.3 предпочтительнее, т.к. оператор чтения данных вернул на 5 строк меньше. Остальные параметры Execution Plan идентичны.
2. Сравнение выполняется по полю Country. По полю Country, по которому производится фильтрация, отсутствует индекс в таблице Customers. 
Для ускорения поиска, с использование индекса (Index seek) необходимо добавить индекс по полю полю Country. 
*/

--SET SHOWPLAN_TEXT ON
--GO

SELECT T.CustomerID, T.Country 
FROM Northwind.dbo.Customers AS T 
WHERE T.Country LIKE '[b-g]%'
ORDER BY T.Country 

--[ki. accepted]
/*4	Использование оператора LIKE
4.1	В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'. 
Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - найти все продукты, которые удовлетворяют этому условию.
Подсказка: результаты запроса должны высвечивать 2 строки.
*/

SELECT T.ProductName 
FROM Northwind.dbo.Products AS T 
WHERE T.ProductName LIKE 'cho_olade'

--[ki. Not accepted "Arithmetic overflow error converting float to data type numeric."] 
/*5	Использование агрегатных функций (SUM, COUNT)
5.1	Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним. 
Результат округлить до сотых и высветить в стиле 1 для типа данных money.  Скидка (колонка Discount) составляет процент из стоимости для данного товара. 
Для определения действительной цены на проданный продукт надо вычесть скидку из указанной в колонке UnitPrice цены. 
Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
*/

SELECT 
	CAST(CAST(SUM(T.UnitPrice*(1-T.Discount)*T.Quantity ) AS numeric (10,2)) AS money) Totals
FROM Northwind.dbo.[Order Details] AS T

-- ki. я бы так написал DECLARE @money_style int = 1
/* SELECT 
	CONVERT (nvarchar(100), CAST(SUM (OD.UnitPrice * (1 - OD.Discount) * Quantity) as money), @money_style) as Totals
FROM dbo.[Order Details] OD*/

--[ki. Accepted]
/*5.2	По таблице Orders найти количество заказов, которые еще не были доставлены (т.е. в колонке ShippedDate нет значения даты доставки).
Использовать при этом запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.
*/

SELECT 
	COUNT(*) - COUNT(T.ShippedDate) CountOrders
FROM Northwind.dbo.[Orders] AS T

--[ki. accepted]
/*5.3	По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/

SELECT 
	COUNT(DISTINCT T.CustomerID) CountCustomerID
FROM Northwind.dbo.[Orders] AS T

--[ki. accepted]
/*6	Явное соединение таблиц, самосоединения, использование агрегатных функций и предложений GROUP BY и HAVING 
6.1	По таблице Orders найти количество заказов с группировкой по годам. 
В результатах запроса надо высвечивать две колонки c названиями Year и Total. Написать проверочный запрос, который вычисляет количество всех заказов.
*/

SELECT  CAST(YEAR(T.OrderDate) AS char) Year, 
		COUNT(DISTINCT T.OrderID) Total
FROM Northwind.dbo.[Orders] AS T 
GROUP BY YEAR(T.OrderDate)

UNION 

SELECT  'ALL' Year, 
		COUNT(DISTINCT T.OrderID) Total
FROM Northwind.dbo.[Orders] AS T 

--[ki. Accepted]
/*6.2 По таблице Orders найти количество заказов, cделанных каждым продавцом.
Заказ для указанного продавца – это любая запись в таблице Orders, где в колонке EmployeeID задано значение для данного продавца. 
В результатах запроса надо высвечивать колонку с именем продавца (Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного запроса. 
Также основной запрос должен использовать группировку по EmployeeID.) с названием колонки ‘Seller’ и колонку c количеством заказов высвечивать с названием 'Amount'. 
Результаты запроса должны быть упорядочены по убыванию количества заказов. 
*/

SELECT 
	(SELECT Employees.LastName + ' '+ Employees.FirstName
	FROM Northwind.dbo.Employees
	WHERE Employees.EmployeeID = T.EmployeeID ) Seller, 
	COUNT(T.OrderID) Amount
FROM Northwind.dbo.[Orders] AS T 
GROUP BY T.EmployeeID
ORDER BY COUNT(T.OrderID) DESC

--[ki. accepted]
/*6.3	По таблице Orders найти количество заказов, cделанных каждым продавцом и для каждого покупателя.
Необходимо определить это только для заказов сделанных в 1998 году.
В результатах запроса надо высвечивать колонку с именем продавца (название колонки ‘Seller’), колонку с именем покупателя (название колонки ‘Customer’) 
и колонку c количеством заказов высвечивать с названием 'Amount'. 
В запросе необходимо использовать специальный оператор языка T-SQL для работы с выражением GROUP (Этот же оператор поможет выводить строку “ALL” в результатах запроса). 
Группировки должны быть сделаны по ID продавца и покупателя. Результаты запроса должны быть упорядочены по продавцу, покупателю и по убыванию количества продаж.
В результатах должна быть сводная информация по продажам. 
Т.е. в резульирующем наборе должны присутствовать дополнительно к информации о продажах продавца для каждого покупателя следующие строчки:
Seller		Customer	Amount
ALL 		ALL		<общее число продаж>
<имя>		ALL		<число продаж для данного продавца>
ALL		<имя>		<число продаж для данного покупателя>
<имя>		<имя>		<число продаж данного продавца для даннного покупателя>
*/

SELECT 
	ISNULL((SELECT Employees.LastName + ' '+ Employees.FirstName
	FROM Northwind.dbo.Employees
	WHERE Employees.EmployeeID = T.EmployeeID ), 'ALL') Seller, 
	ISNULL((SELECT Customers.ContactName
	FROM Northwind.dbo.Customers
	WHERE Customers.CustomerID = T.CustomerID ), 'ALL') Customer, 
	COUNT(T.OrderID) Amount
FROM Northwind.dbo.[Orders] AS T 
WHERE DATEPART(yyyy,T.OrderDate) = N'1998'
GROUP BY CUBE  (T.EmployeeID, T.CustomerID)
ORDER BY COUNT(T.OrderID) DESC

-- [ki. accepted]
/* 6.4	Найти покупателей и продавцов, которые живут в одном городе. Если в городе живут только один или несколько продавцов или только один или несколько покупателей,
то информация о таких покупателя и продавцах не должна попадать в результирующий набор. Не использовать конструкцию JOIN. 
В результатах запроса необходимо вывести следующие заголовки для результатов запроса: ‘Person’, ‘Type’ (здесь надо выводить строку ‘Customer’ или  ‘Seller’ в завимости от типа записи),
‘City’. Отсортировать результаты запроса по колонке ‘City’ и по ‘Person’.
*/

SELECT	T.LastName + ' '+ T.FirstName AS Person, 
		'Seller' AS Type, 
		T.City AS City
FROM Northwind.dbo.Employees AS T
WHERE T.City  IN (
	SELECT	City
	FROM (SELECT DISTINCT City 
		FROM Northwind.dbo.Employees

		UNION ALL

		SELECT DISTINCT City 
		FROM Northwind.dbo.Customers ) AS TCity
	GROUP BY City
	HAVING COUNT(*) > 1)

UNION ALL

SELECT	T.ContactName, 
		'Customer', 
		T.City
FROM Northwind.dbo.Customers AS T
WHERE T.City  IN (
	SELECT	City
	FROM (SELECT DISTINCT City 
		FROM Northwind.dbo.Employees

		UNION ALL

		SELECT DISTINCT City 
		FROM Northwind.dbo.Customers ) AS TCity
	GROUP BY City
	HAVING COUNT(*) > 1)
ORDER BY City, Person

--[ki. accepted]
/* 6.5	Найти всех покупателей, которые живут в одном городе. В запросе использовать соединение таблицы Customers c собой - самосоединение. 
Высветить колонки CustomerID и City. Запрос не должен высвечивать дублируемые записи. Для проверки написать запрос, который высвечивает города, которые встречаются более одного раза в таблице Customers.
Это позволит проверить правильность запроса.
*/

SELECT DISTINCT 
		T.CustomerID, 
		T.City
FROM Northwind.dbo.Customers AS T
INNER JOIN Northwind.dbo.Customers AS T0
ON T0.CustomerID<>T.CustomerID AND T0.City=T.City

-- Проверочный запрос: высвечивает города, которые встречаются более одного раза в таблице Customers.
SELECT City, COUNT(*) 
FROM Northwind.Customers 
GROUP BY City
HAVING COUNT(*) > 1

--[ki. accepted]
/*6.6	По таблице Employees найти для каждого продавца его руководителя, т.е. кому он делает репорты. 
Высветить колонки с именами 'User Name' (LastName) и 'Boss'. В колонках должны быть высвечены имена из колонки LastName. Высвечены ли все продавцы в этом запросе?

--Высвечены ли все продавцы в этом запросе? 
--Ответ: Нет, не вышел продавец у которого нет босса (ReportsTo = NULL).
*/

SELECT DISTINCT	T.LastName AS [User Name], 
		T0.LastName AS Boss
FROM Northwind.dbo.Employees AS T
INNER JOIN Northwind.dbo.Employees AS T0
ON T.ReportsTo=T0.EmployeeID 

--[ki. Accepted]
/*7	Использование Inner JOIN
7.1	Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
Результаты запроса должны высвечивать два поля: 'LastName' продавца и название обслуживаемой территории ('TerritoryDescription' из таблицы Territories).
Запрос должен использовать JOIN в предложении FROM. Для определения связей между таблицами Employees и Territories надо использовать графические диаграммы для базы Northwind.
*/

SELECT	E.LastName, 
		T.TerritoryDescription
FROM Northwind.dbo.EmployeeTerritories AS ET 
	INNER JOIN Northwind.dbo.Employees AS E
	ON E.EmployeeID = ET.EmployeeID
	INNER JOIN Northwind.dbo.Territories AS T
	ON T.TerritoryID = ET.TerritoryID
		INNER JOIN Northwind.dbo.Region AS R
		ON R.RegionID = T.RegionID 
WHERE R.RegionDescription='Western'

--[ki. accepted]
/*8	Использование Outer JOIN
8.1	Высветить в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество их заказов из таблицы Orders. 
Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса. 
Упорядочить результаты запроса по возрастанию количества заказов.
*/

SELECT	C.ContactName, 
		COUNT(O.OrderID) CountOrder
FROM Northwind.dbo.Customers AS C
	LEFT OUTER JOIN Northwind.dbo.Orders AS O 
	ON C.CustomerID=O.CustomerID
GROUP BY C.ContactName
ORDER BY COUNT(O.OrderID)

--[ki. Accepted]
/*9	Использование подзапросов
9.1	Высветить всех поставщиков колонка CompanyName в таблице Suppliers, у которых нет хотя бы одного продукта на складе (UnitsInStock в таблице Products равно 0).
Использовать вложенный SELECT для этого запроса с использованием оператора IN. 
Можно ли использовать вместо оператора IN оператор '=' ?

Ответ: В данной задаче нельзя, т.к. вложенный запрос возвращает больше одного значения.
*/

SELECT T.CompanyName
FROM Northwind.dbo.Suppliers AS T
WHERE T.SupplierID IN 
	(SELECT P.SupplierID 
	FROM Northwind.dbo.Products AS P
	WHERE P.UnitsInStock=0)

/*10	Коррелированный запрос
10.1	Высветить всех продавцов, которые имеют более 150 заказов. Использовать вложенный коррелированный SELECT.
*/

SELECT E.LastName + ' '+ E.FirstName AS Seller
FROM Northwind.dbo.Employees AS E
WHERE (SELECT COUNT(DISTINCT O.OrderID) 
	FROM Northwind.dbo.Orders AS O 
	WHERE O.EmployeeID = E.EmployeeID) > 150

-- [ki. accepted]
/*11	Использование EXISTS
11.1	Высветить всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders). 
Использовать коррелированный SELECT и оператор EXISTS.
*/

SELECT C.ContactName AS Customer
FROM Northwind.dbo.Customers AS C
WHERE NOT EXISTS 
	(SELECT O.OrderID
	FROM Northwind.dbo.Orders AS O 
	WHERE O.CustomerID = C.CustomerID)

/*12	Использование строковых функций
12.1	Для формирования алфавитного указателя Employees высветить из таблицы Employees список только тех букв алфавита,
с которых начинаются фамилии Employees (колонка LastName ) из этой таблицы.
Алфавитный список должен быть отсортирован по возрастанию.
*/

SELECT DISTINCT SUBSTRING(E.LastName, 1, 1) AS FirstChar
FROM Northwind.Employees AS E
ORDER BY FirstChar

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

-- Вызов процедуры
DECLARE @year int = '1998';  
EXEC dbo.GreatestOrders @year, 5 

-- Проверочный запрос
SELECT 
	(SELECT Employees.FirstName+ ' '+ Employees.LastName 
	FROM Northwind.Employees
	WHERE Employees.EmployeeID = O.EmployeeID ) Seller,
	O.OrderID, 
	SUM(CONVERT(decimal(14,2), OD.Quantity * (1-OD.Discount) * OD.UnitPrice)) Totals
FROM [Northwind].Orders AS O
LEFT JOIN [Northwind].[Order Details] AS OD
ON O.OrderID = OD.OrderID 
WHERE 
	DATEPART(yyyy,O.OrderDate) = @year 
GROUP BY O.EmployeeID, O.OrderID
ORDER BY Totals DESC

/*13.2	Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку доставки в днях (разница между OrderDate и ShippedDate).  
В результатах должны быть возвращены заказы, срок которых превышает переданное значение или еще недоставленные заказы. 
Значению по умолчанию для передаваемого срока 35 дней. Название процедуры ShippedOrdersDiff.
Процедура должна высвечивать следующие колонки: OrderID, OrderDate, ShippedDate, ShippedDelay (разность в днях между ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение). 
Необходимо продемонстрировать использование этой процедуры.
*/

EXEC dbo.ShippedOrdersDiff  @dayShipped = 25 

/*13.3	 Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, так и подчиненных его подчиненных. 
В качестве входного параметра функции используется EmployeeID. Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать оператор PRINT) согласно иерархии подчинения.
Продавец, для которого надо найти подчиненных также должен быть высвечен. Название процедуры SubordinationInfo. 
В качестве алгоритма для решения этой задачи надо использовать пример, приведенный в Books Online и рекомендованный Microsoft для решения подобного типа задач. 
Продемонстрировать использование процедуры.
*/
EXEC dbo.SubordinationInfo @EmployeeID = 2 

/*13.4	 Написать функцию, которая определяет, есть ли у продавца подчиненные. Возвращает тип данных BIT. В качестве входного параметра функции используется EmployeeID. 
Название функции IsBoss. Продемонстрировать использование функции для всех продавцов из таблицы Employees.
*/

SELECT  E.FirstName+ ' '+ E.LastName Title,
	dbo.IsBoss(E.EmployeeID) AS IsBoss
FROM Northwind.Employees AS E

/*
14	Работа по финальному проекту
На основе диаграммы классов проработайте архитектуру базы данных вашего финального проекта.
Напишите скрипт создания сущностей пользователя, ролей и зависимых сущностей (достаточных для выполнения CRUD операций над пользователями и выдачи им определенных ролей)
*Напишите скрипт создания оставшихся сущностей вашей диаграммы. 
*/


USE [master]
GO

IF DB_ID (N'Messenger') IS NOT NULL
DROP DATABASE Messenger;
GO

IF object_ID('Users') is not null
   DROP TABLE [dbo].[Users]
GO 

IF object_ID('Attachments') is not null
   DROP TABLE [dbo].[Attachments]
GO 

IF object_ID('ChatMessage') is not null
   DROP TABLE [dbo].[ChatMessage]
GO 

IF object_ID('Chats') is not null
   DROP TABLE [dbo].[Chats]
GO 

IF object_ID('UsersOfChats') is not null
   DROP TABLE [dbo].[UsersOfChats]
GO 

IF object_ID('Roles') is not null
   DROP TABLE [dbo].[Roles]
GO 

CREATE DATABASE [Messenger]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Messenger', FILENAME = N'D:\MSSQLServer\MSSQL14.SQLEXPRESS\MSSQL\DATA\Messenger.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Messenger_log', FILENAME = N'D:\MSSQLServer\MSSQL14.SQLEXPRESS\MSSQL\DATA\Messenger_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

USE [Messenger]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachments](
	[AttachID] [int] NOT NULL,
	[ImageAttach] [image] NOT NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[AttachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatMessage](
	[MessageID] [int] NOT NULL,
	[ChatID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[AttachID] [int] NOT NULL,
 CONSTRAINT [PK_ChatMessage] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chats](
	[ChatID] [int] NOT NULL,
	[Chatname] [nvarchar](max) NOT NULL,
	[DateOfCreation] [datetime] NOT NULL,
 CONSTRAINT [PK_Chats] PRIMARY KEY CLUSTERED 
(
	[ChatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

USE [Messenger]
GO

CREATE TABLE [dbo].[Roles](
	[RoleID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[DateOfCreation] [datetime] NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersOfChats](
	[UserID] [int] NOT NULL,
	[ChatID] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChatMessage]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessage_Chats] FOREIGN KEY([ChatID])
REFERENCES [dbo].[Chats] ([ChatID])
GO
ALTER TABLE [dbo].[ChatMessage] CHECK CONSTRAINT [FK_ChatMessage_Chats]
GO
ALTER TABLE [dbo].[ChatMessage]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessage_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[ChatMessage] CHECK CONSTRAINT [FK_ChatMessage_Users]
GO
ALTER TABLE [dbo].[ChatMessage]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessage_Attach] FOREIGN KEY([AttachID])
REFERENCES [dbo].[Attachments] ([AttachID])
GO
ALTER TABLE [dbo].[ChatMessage] CHECK CONSTRAINT [FK_ChatMessage_Attach]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[UsersOfChats]  WITH CHECK ADD  CONSTRAINT [FK_UsersOfChats_Chats] FOREIGN KEY([ChatID])
REFERENCES [dbo].[Chats] ([ChatID])
GO
ALTER TABLE [dbo].[UsersOfChats] CHECK CONSTRAINT [FK_UsersOfChats_Chats]
GO
ALTER TABLE [dbo].[UsersOfChats]  WITH CHECK ADD  CONSTRAINT [FK_UsersOfChats_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UsersOfChats] CHECK CONSTRAINT [FK_UsersOfChats_Users]
GO
USE [master]
GO
ALTER DATABASE [Messenger] SET  READ_WRITE 
GO
