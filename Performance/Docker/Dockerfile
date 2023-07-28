# Stage 1: Build  and run the code
FROM grafana/k6:latest

USER root

RUN apk add --update git

WORKDIR /app

# Clone your test project repository
RUN git clone https://github.com/eneskuehn/Dockerized.git

WORKDIR /app/Dockerized/Performance

CMD ["run", "SmokeTest.js"]