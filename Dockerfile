FROM node:12.13.1-alpine
ADD . /app/
WORKDIR /app
RUN npm install
ENV NODEJSENVIRONMENT dockerLocal

# EXPOSE 2368
VOLUME /app/logs

CMD npm run docker