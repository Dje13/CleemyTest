INSERT INTO [dbo].[LuccaUser]
           ([firstName]
           ,[lastName]
           ,[currencyId])
		   SELECT 'Anthony','Stark',id
		   from [Currency] where code='USD'


INSERT INTO [dbo].[LuccaUser]
           ([firstName]
           ,[lastName]
           ,[currencyId])
		   SELECT 'Natasha','Romanova',id
		   from [Currency] where code='RUB'

INSERT INTO [dbo].[Expense]
           ([dateExpense]
           ,[natureId]
           ,[luccaUserId]
           ,[currencyId]
           ,[amount]
           ,[commentExpense])
SELECT GETDATE()-1,N.id,u.id,c.id,10,'test commentair'
FROM Nature N JOIN LuccaUser U ON U.firstName='Anthony' JOIN currency C ON  C.code ='USD' 
WHERE n.[name] = 'Restaurant'

