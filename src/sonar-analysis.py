import os
import subprocess


def is_tool_installed(tool_name):
    result = subprocess.run(["dotnet", "tool", "list", "--global"], capture_output=True)
    return tool_name in result.stdout.decode()


def run_command(cmd):
    result = os.system(cmd)
    if result != 0:
        raise Exception(f"Failed to run command: {cmd}")


tools = ["dotnet-sonarscanner", "coverlet.console", "dotnet-coverage"]
for tool in tools:
    if not is_tool_installed(tool):
        run_command(f"dotnet tool install --global {tool}")

commands = [
    "dotnet sonarscanner begin /d:sonar.login=admin /d:sonar.password=corpspace /k:\"Corpspace\" "
    "/d:sonar.host.url=\"http://localhost:9001\" /s:\"`pwd`/SonarQube.Analysis.xml\"",
    "dotnet build Corpspace.sln",
    "dotnet sonarscanner end /d:sonar.login=admin /d:sonar.password=corpspace"
]

for command in commands:
    run_command(command)
