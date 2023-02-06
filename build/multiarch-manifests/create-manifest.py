import sys
import subprocess


def main(registry):
    if not registry:
        print("Registry must be set to docker registry to use")
        sys.exit(1)

    print("This script creates the local manifests, for pushing the multi-arch manifests")
    print("Tags used are linux-main, win-main, linux-develop, win-develop, linux-latest, win-latest")
    print("Multi-arch images tags will be master, dev, latest")

    services = ["identity.api", "payment.api",
                "webhooks.api", "mobiles-agg", "web-agg",
                "webstatus", "webspa", "webhooks.client"]

    for svc in services:
        print("Creating manifest for {} and tags :latest, :main, and :develop".format(svc))
        subprocess.call(
            "docker manifest create {}/{}:master {}/{}:linux-main {}/{}:win-main".format(registry, svc, registry, svc,
                                                                                         registry, svc), shell=True)
        subprocess.call(
            "docker manifest create {}/{}:develop {}/{}:linux-develop {}/{}:win-develop".format(registry, svc, registry,
                                                                                                svc, registry, svc),
            shell=True)
        subprocess.call(
            "docker manifest create {}/{}:latest {}/{}:linux-latest {}/{}:win-latest".format(registry, svc, registry,
                                                                                             svc, registry, svc),
            shell=True)
        print("Pushing manifest for {} and tags :latest, :main, and :develop".format(svc))
        subprocess.call("docker manifest push {}/{}:latest".format(registry, svc), shell=True)
        subprocess.call("docker manifest push {}/{}:develop".format(registry, svc), shell=True)
        subprocess.call("docker manifest push {}/{}:main".format(registry, svc), shell=True)


if __name__ == '__main__':
    main(sys.argv[1] if len(sys.argv) > 1 else None)
