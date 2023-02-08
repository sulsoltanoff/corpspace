from diagrams import Diagram, Cluster
from diagrams.aws.compute import Lambda
from diagrams.aws.devtools import CLI
from diagrams.aws.general import MobileClient, Client
from diagrams.digitalocean.compute import Docker
from diagrams.ibm.network import Gateway
from diagrams.onprem.database import PostgreSQL
from diagrams.onprem.network import Nginx
from diagrams.onprem.queue import RabbitMQ

with Diagram("Microservice Architecture"):
    with Cluster("Clients"):
        mobile = MobileClient("Mobile App")
        desktop = Client("Web App (Desktop)")
        api = CLI("API")

    with Cluster("Frontend Infrastructure"):
        nginx = Nginx("Nginx")
        apigateway = Gateway("API Gateway")

    with Cluster("Backend for Frontend"):
        bff = Lambda("BFF Web and mobile")
        bffdb = PostgreSQL("BFF DB")
        bff >> bffdb

    with Cluster("Auth Service"):
        auth = Docker("Auth")
        authdb = PostgreSQL("Auth DB")
        auth >> authdb

    with Cluster("Chat Service"):
        chat = Docker("Chat")
        chatdb = PostgreSQL("Chat DB")
        chat >> chatdb

    with Cluster("Payment Service"):
        payment = Docker("Payment")
        paymentdb = PostgreSQL("Payment DB")
        payment >> paymentdb

    with Cluster("Wiki Service"):
        wiki = Docker("Wiki")
        wikidb = PostgreSQL("Wiki DB")
        wiki >> wikidb

    with Cluster("Git Hosting Service"):
        git = Docker("Git")
        gitdb = PostgreSQL("Git DB")
        git >> gitdb

    with Cluster("Calendar Service"):
        calendar = Docker("Calendar")
        calendardb = PostgreSQL("Calendar DB")
        calendar >> calendardb

    with Cluster("Project Management Service"):
        pms = Docker("PMS")
        pmsdb = PostgreSQL("PMS DB")
        pms >> pmsdb

    with Cluster("RabbitMQ"):
        rabbitmq = RabbitMQ("RabbitMQ")

    allServices = [auth, chat, wiki, git, payment, calendar, pms]

    for i in range(len(allServices)):
        for j in range(i + 1, len(allServices)):
            allServices[i] >> allServices[j]

    mobile >> nginx >> apigateway >> bff >> allServices
    desktop >> nginx >> apigateway >> bff >> allServices
    api >> nginx >> apigateway >> bff >> allServices
    allServices >> rabbitmq
