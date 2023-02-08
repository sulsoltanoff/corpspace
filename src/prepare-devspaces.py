import os

apps = "app.yaml"
inf = "inf.yaml"


def copy_files(src_dirs, dest_dir, file_name):
    src_file = os.path.join(src_dirs, file_name)
    dest_file = os.path.join(dest_dir, file_name)
    if os.path.exists(src_file):
        print(f"Copying {file_name} to {dest_dir}")
        os.makedirs(dest_dir, exist_ok=True)
        os.system(f"cp {src_file} {dest_file}")


src_dir = "../deploy/k8s/helm"

copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Mobile/apigw", apps)
copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Mobile/apigw", inf)

# copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Web/apigw", "app.yaml")
# copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Web/apigw", "inf.yaml")

copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Mobile/aggregator", apps)
copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Mobile/aggregator", inf)

copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Web/aggregator", apps)
copy_files(src_dir, "./ApiGateways/Corpspace.Bff.Web/aggregator", inf)

copy_files(src_dir, "./Services/Identity/Identity.API", apps)
copy_files(src_dir, "./Services/Identity/Identity.API", inf)

copy_files(src_dir, "./Services/Payment/Payment.API", apps)
copy_files(src_dir, "./Services/Payment/Payment.API", inf)

copy_files(src_dir, "./Services/Webhooks/Webhooks.API", apps)
copy_files(src_dir, "./Services/Webhooks/Webhooks.API", inf)
