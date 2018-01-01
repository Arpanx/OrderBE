
 https://youtu.be/yh_7eFwG3QM   Demo video how it works

1) git clone https://github.com/Arpanx/OrderBE.git
2) cd OrderBE\src\MyOrder.API
3) dotnet restore 
4) dotnet build
5) see  appsettings.json & edit ConnectionStrings as you need (MS SQL 2017 Express)  
    "ConnectionStrings": {
		"OrderContext": "Server=localhost\\SQLEXPRESS01;Database=Order;Trusted_Connection=True;MultipleActiveResultSets=true"
    },
6) dotnet ef database update  
7) dotnet run
8) see  http://localhost:5000

