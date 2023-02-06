import re


def create_dockerfile_restore(solution_file: str = "Corpspace.sln"):
    outfile = "DockerfileSolutionRestore.txt"
    solution = solution_file

    with open(outfile, "w") as f:
        f.write("COPY \"" + solution + "\" \"" + solution + "\"\n\n")

        with open(solution, "r") as s:
            contents = s.read()

            csproj_files = sorted(re.findall(r', "(.*?\.csproj)"', contents))
            for csproj in csproj_files:
                csproj = csproj.replace("\\", "/")
                f.write("COPY \"" + csproj + "\" \"" + csproj + "\"\n")

            dcproj_files = re.findall(r', "(.*?\.dcproj)"', contents)
            for dcproj in dcproj_files:
                dcproj = dcproj.replace("\\", "/")
                f.write("\nCOPY \"" + dcproj + "\" \"" + dcproj + "\"\n")

        f.write("\n")
        f.write("COPY \"NuGet.config\" \"NuGet.config\"\n")
        f.write("\n")
        f.write("RUN dotnet restore \"" + solution + "\"\n")


create_dockerfile_restore()
