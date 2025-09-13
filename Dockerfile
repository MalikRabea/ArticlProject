# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# نسخ ملفات المشاريع والحل
COPY ArticlPro.sln ./
COPY ArticlPro.Core/*.csproj ./ArticlPro.Core/
COPY ArticlPro.Data/*.csproj ./ArticlPro.Data/
COPY ArticlPro.Test/*.csproj ./ArticlPro.Test/

# استرجاع البكجات
RUN dotnet restore

# نسخ باقي الملفات
COPY . .

# بناء المشروع (Release)
RUN dotnet publish ArticlPro.sln -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# نسخ الناتج من مرحلة البناء
COPY --from=build /app/publish .

# تعيين المنفذ (غيره إذا مشروعك يستخدم منفذ آخر)
EXPOSE 5000

# الأمر الافتراضي لتشغيل التطبيق
ENTRYPOINT ["dotnet", "ArticlPro.dll"]
