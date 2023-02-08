import os
import requests
import json
import csv

url = "http://localhost:9001/api/issues/search?s=FILE_LINE&p=1&ps=500&status=TO_REVIEW&onlyMine=false&sinceLeakPeriod" \
      "=false"

auth_token = os.environ.get("CSP_SONAR_TOKEN")

response = requests.get(url, auth=tuple(auth_token.split(':')))

if response.status_code == 200:
    data = json.loads(response.text)
    issues = data['issues']

    supported_formats = ['csv', 'html', 'json', 'markdown']
    file_format = input(f"Enter the file format to export the data in ({', '.join(supported_formats)}): ")

    while file_format not in supported_formats:
        file_format = input(
            f"Invalid file format. Enter the file format to export the data in ({', '.join(supported_formats)}): ")

    if file_format == 'csv':
        fieldnames = ['component', 'line', 'message', 'severity']
        with open('issues.csv', 'w', newline='') as csvfile:
            writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
            writer.writeheader()
            for issue in issues:
                writer.writerow({
                    'component': issue['component'],
                    'line': issue['line'],
                    'message': issue['message'],
                    'severity': issue['severity'],
                })

    elif file_format == 'html':
        with open('issues.html', 'w') as html_file:
            html_file.write("<html><head><style>table, th, td { border: 1px solid black; border-collapse: collapse; "
                            "}</style></head><body><table><tr><th>Component</th><th>Line</th><th>Message"
                            "</th><th>Severity</th></tr>")
            for issue in issues:
                html_file.write("<tr><td>{}</td><td>{}</td><td>{}</td><td>{}</td></tr>".format(
                    issue['component'], issue['line'], issue['message'], issue['severity']
                ))
            html_file.write("</table></body></html>")

    elif file_format == 'json':
        with open('issues.json', 'w') as jsonfile:
            json.dump(issues, jsonfile, indent=2)

    elif file_format == 'markdown':
        with open('issues.md', 'w') as md_file:
            md_file.write("# Issues from SonarQube\n\n")
            md_file.write("Component | Line | Message | Severity |\n")
            md_file.write("--- | --- | --- | --- |\n")
            for issue in issues:
                md_file.write("| {} | {} | {} | {} |\n".format(
                   issue['component'], issue['line'], issue['message'], issue['severity']
                ))
