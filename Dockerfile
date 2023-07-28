FROM mcr.microsoft.com/dotnet/sdk:6.0

# Install Git
RUN apt-get update 
RUN apt-get install -y git


WORKDIR /app
# Clone your test project repository
RUN git clone https://github.com/eneskuehn/Dockerized.git


WORKDIR /app/Dockerized

RUN dotnet build 

RUN apt-get update

CMD ["dotnet", "test"]