rem @echo off
set amout=%TIME:~9,2%
C:\Install\curl-7.74.0_2-win64-mingw\bin\curl.exe -i -X POST http://localhost:51570/api/expense -H "Content-Type: application/json" -d "{\"nature\":\"Restaurant\", \"dateExpense\":\"2020-12-12\",\"amount\":"%amout%",\"commentExpense\":\"test\",\"luccaUserId\":1,\"currency\":\"USD\"}"
C:\Install\curl-7.74.0_2-win64-mingw\bin\curl.exe -i -X POST http://localhost:51570/api/expense -H "Content-Type: application/json" -d "{\"nature\":\"Restaurant\", \"dateExpense\":\"2020-12-12\",\"amount\":"%amout%",\"commentExpense\":\"test\",\"luccaUserId\":2,\"currency\":\"RUB\"}"
pause