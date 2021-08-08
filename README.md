# Pokedex

Submission from Joel Oughton.

## Minimum requirements

- .NET Core 3.1 runtime (https://dotnet.microsoft.com/download/dotnet/3.1/runtime)

## Run instructions

- Open terminal 
- Run `cd .\Pokedex\Api`
- Run `dotnet run`
- Execute HTTP requests (e.g., http://localhost:5000/pokemon/mewtwo)

## Notes

For development usage only.

In a production environment the following would be done,

- Use HTTPS only.
- Retry support of third part API requests.
- Return result classes instead of throwing in third party API services.
- Add a .NET background service to store all Pokemon into a database and use that as the source for the API request instead of hitting the PokeApi directly.
- If the expectation is very high request traffic then add a in-memory cache of pokemon data that is invalidated when the load Pokemon background process runs.
- The Fun Translations API is very limited in the throughput allowance for free accounts. Given Pokemon descriptions do not change often, store translations for each Pokemon description as part of the background process. Then fetch translations from the database instead.
- Add component tests to validate API contracts.