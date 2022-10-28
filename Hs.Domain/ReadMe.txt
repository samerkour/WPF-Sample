
In order to scaffold databese entities follow steps

1- open cmd in Administrator mode
2- cd to Domain project where you want to create the entities
3- modefy the command below (data base connction and entities folder) then run it

	dotnet ef dbcontext scaffold "Data Source=DESKTOP-53A5H27\LOCALSERVER;Initial Catalog=SampleDb;user id=sa;password=Admin@123" Microsoft.EntityFrameworkCore.SqlServer -o Entities/SampleDbEntities
	