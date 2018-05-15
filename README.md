## AssociateTool
associate tool in line

* id: id tool type.
  * Integer
  * Required
* name: name of tool type
  * String (50 characters)
  * Required
* description: description of tool type
  * String (100 characters)
  * Optional
* serialNumber: serial number of tool
  * String (100 characters)
  * Optional
* code: code of tool
  * String (100 characters)
  * Optional
* lifeCycle: life time total
  * Cannot be changed
  * Required
* currentLife: life consumed of tool
  * Double
  * Required
* unitOfMeasurement: unit of measurement of life
  * Cannot be changed
  * Required
* position: position of the tool
  * Nullable Integer  
* typeId: id of tool type
  * Integer
  * Required
* typeName: name of tool type
  * String
  * used only get (informative)
* status: status of tool
* currentThingId: Id of the current Thing that is using the tool
  * Ignored on Create and Update
  * Can only be changed when associating tools with things

```json
  "tool": {
    "id": 1,
    "name": "Ferramenta",
    "description": "TESTE ferramenta",
    "serialNumber": "23123",
    "code": "2323",
    "lifeCycle": 100,
    "currentLife": 0,
    "unitOfMeasurement": "minute",
    "typeId": 1,
    "typeName": "Tipo",
    "status": "available"
  },
```
##  Url ToolType
api/associate
* POST: Associate tool in position
	* COMMENTS: If possition is null associate, if position is not null disassociate.
