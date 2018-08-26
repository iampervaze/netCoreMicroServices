cd src
docker build -f ./Action.Api/Dockerfile -t action.api ./Action.Api
docker build -f ./Action.Services.Activities/Dockerfile -t action.services.activities ./Action.Services.Activities
docker build -f ./Action.Services.Identity/Dockerfile -t action.services.identity ./Action.Services.Identity