FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11
LABEL pipelineName="ScoreService" \
      pipelineKey="WIPKZEBH" \
      offeringKey="JTJZKHWF"
RUN apk upgrade -U
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
WORKDIR /app
COPY . /app
USER guest
ENTRYPOINT ["dotnet", "HCEHB.ScoreService.Web.dll"] 