
 https://youtu.be/yh_7eFwG3QM   Demo video how it works

1) git clone https://github.com/Arpanx/OrderBE.git
2) cd Order
3) dotnet build
4) see  appsettings.json & edit ConnectionStrings as you need (MS SQL 2017 Express)  
    "ConnectionStrings": {
		"OrderContext": "Server=localhost\\SQLEXPRESS01;Database=Order;Trusted_Connection=True;MultipleActiveResultSets=true"
    },
5) dotnet ef database update
6) dotnet run
7) see  http://localhost:5000

