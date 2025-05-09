FROM node:18-alpine as build
WORKDIR /app

COPY . .
RUN npm install && npm run build 

# Nginx to serve the built Angular app
FROM nginx:alpine
COPY --from=build /app/dist/task-management-app /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
