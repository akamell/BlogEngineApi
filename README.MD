# HOW TO RUN THE PROJECT?

### CLONE THE REPO
git clone https://github.com/akamell/BlogEngineApi.git

### INIT USER SECRETS TO DEV ENV
dotnet user-secrets init
dotnet user-secrets set "BlogEngine:ConnectionString": "Data Source=myHost;database=MyDatabase;user=myUser;pwd=myPassword;Min Pool Size=5;Max Pool Size=25;Connection Reset=True;Connection Lifetime=300;Convert Zero Datetime=True;"

### INSTALL DOTNET EF TOOL
dotnet tool install --global dotnet-ef

### EXECUTE MIGRATIONS
dotnet ef database update

### RUN
dotnet run 

# API Doc
POST https://localhost:5001/user/login

If you want to be a Writer user, you must send the following as a payload
{
	"UserName": "Writer"
}

or if you want to be a Editor
{
    "UserName": "Editor"
}

GET https://localhost:5001/post/pending-approve
return all posts pending approval

GET https://localhost:5001/post
return all approved posts

GET https://localhost:5001/post/{id}
return a post by id

POST https://localhost:5001/post
{
	"Content": "New Content"
}
create a new post, it's required send Token as Writer.

PUT https://localhost:5001/post/{id}/approve
{
    "Status": 1 to approve, 2 to reject
}
It's required send token as Editor

PUT https://localhost:5001/post/{id}
{
    "Content": "New Content"
}
Update the content of post when the post was rejected, It's required send token as Write.

DELETE https://localhost:5001/post/{id}
Delete a post, it's required send token as Editor.
