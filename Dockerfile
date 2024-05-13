
# Use the .NET Core SDK image as the base image
FROM FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the container
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code to the container
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Create the final image using the .NET Core runtime image
FROM FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

WORKDIR /app
COPY --from=build-env /app/out .


# Run the application
#ENTRYPOINT ["dotnet", "dotnetwebapp.dll"]
ENTRYPOINT ["dotnet", "dotnetwebapp.dll", "--urls", "http://*:5000"]
