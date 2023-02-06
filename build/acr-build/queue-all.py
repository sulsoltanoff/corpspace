import os


def queue_all(acr_name=None, git_user=None, repo_name="corpspace", git_branch="main", pat_token=None):
    git_context = f"https://github.com/{git_user}/{repo_name}"
    services = [
        {"Name": "corpspace-identity", "Image": "corpspace/identity.api", "File": "src/Services/Identity/Identity.API"
                                                                                  "/Dockerfile"},
        {"Name": "corpspace-spa", "Image": "corpspace/webspa", "File": "src/Web/WebSPA/Dockerfile"},
        {"Name": "corpspace-webstatus", "Image": "corpspace/webstatus", "File": "src/Web/WebStatus/Dockerfile"},
        {"Name": "corpspace-payment", "Image": "corpspace/payment.api", "File": "src/Services/Payment/Payment.API"
                                                                                "/Dockerfile"},
        {"Name": "corpspace-mobile-agg", "Image": "corpspace/mobile-agg", "File": "src/ApiGateways/Corpspace.Bff"
                                                                                  ".Mobile/aggregator/Dockerfile"},
        {"Name": "corpspace-web-agg", "Image": "corpspace/web-agg", "File": "src/ApiGateways/Corpspace.Bff.Web"
                                                                            "/aggregator/Dockerfile"},
    ]

    for service in services:
        bname = service["Name"]
        bimg = service["Image"]
        bfile = service["File"]
        print(f"Setting ACR build {bname} ({bimg})")
        os.system(
            f"az acr build-task create --registry {acr_name} --name {bname} --image {bimg}:{git_branch} --context {git_context} --branch {git_branch} --git-access-token {pat_token} --file {bfile}")


queue_all()
