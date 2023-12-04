when using docker desktop you can run the api and database with the following commands.

build the image locally.
docker build -t oxygenmeasurementapi:latest

run the docker compose file, you might want to change directory to OxygenMeasurementApi project folder.
docker compose up

if you already got the containers running and wants to create them again you can run this command to delete the containers, their network and volume.
docker compose down -v 
