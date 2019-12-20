Database tests require some initial conditions:

1. Create collection called "sdk-collection".
2. Set it's JSON schema and UI schema as follows:

JSON Schema:
```json
{
    "title": "sdk-collection",
    "type": "object",
    "required": [],
    "translatable": [],
    "properties": {
        "number": {
            "type": "integer",
            "title": "Number",
            "order": "None"
        },
        "notes": {
            "type": "string",
            "title": "Notes",
            "order": "None"
        }
    }
}
```

UI Schema:
```json
{
    "number": {
        "ui:widget": "updown"
    },
    "notes": {}
}
```