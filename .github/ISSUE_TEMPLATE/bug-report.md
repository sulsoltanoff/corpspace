name: "Bug report 🐛"
description: Report errors or unexpected behavior
labels: [Issue-Bug, Needs-Triage]
body:
- type: markdown
  attributes:
  value: |
  Please make sure to [search for existing issues](https://github.com/sulsoltanoff/corpspace/issues) and [check the FAQ](https://github.com/sulsoltanoff/corpspace/wiki/Frequently-Asked-Questions-(FAQ)) before filing a new one!

- type: input
  attributes:
  label: Corpspace platform version
  placeholder: "v0.1.0"
  description: |
  You can copy the version number from the About dialog.
  validations:
  required: false

- type: textarea
  attributes:
  label: Other Software
  description: If you're reporting a bug about our interaction with other software, what software? What versions?
  placeholder: |
  Postgresql 14.5
  NodeJS 19
  My Cool Application v0.3 (include a code snippet if it would help!)
  validations:
  required: false

- type: textarea
  attributes:
  label: Steps to reproduce
  placeholder: Tell us the steps required to trigger your bug.
  validations:
  required: true

- type: textarea
  attributes:
  label: Expected Behavior
  description: If you want to include screenshots, paste them into the markdown editor below.
  placeholder: What were you expecting?
  validations:
  required: false

- type: textarea
  attributes:
  label: Actual Behavior
  placeholder: What happened instead?
  validations:
  required: true
