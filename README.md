# thomson

URL para autenticar necessário para realizar as chamadas

https://thomsonreutersapi.azurewebsites.net/api/user/authenticate

```
{
    "Email": "test",
    "Password": "test"
}
```
retorno exemplo:

```
{
    "id": 1,
    "firstName": "Test",
    "lastName": "User",
    "email": "test",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MjU0NDMyNDUsImV4cCI6MTYyNjA0ODA0NSwiaWF0IjoxNjI1NDQzMjQ1fQ.1SpK--7hJW_hB5Mnmmq3hF9MXAamq5ymeWNcZNRujnc"
}
```

Rota das demais chamadas

https://thomsonreutersapi.azurewebsites.net/api/legalcase

