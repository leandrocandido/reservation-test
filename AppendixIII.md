## Appendix III – GET /Reservation

This endpoint returns an existing reservation using the reservation number as parameter.

## Request contract url

/Reservation/ABC123

## Response contract in JSON format

```json
{
  "reservationNumber": "ABC123",
  "email": "contact@contact.com",
  "flights": [
    {
      "flight": "Flight00052",
      "passengers": [
        {
          "name": "Robert Plant",
          "bags": 3,
          "seat": "27"
        },
        {
          "name": "Ozzy Osbourne",
          "bags": 0,
          "seat": "28"
        }
      ]
    },
    {
      "flight": "Flight00103",
      "passengers": [
        {
          "name": "Robert Plant",
          "bags": 2,
          "seat": "41"
        },
        {
          "name": "Ozzy Osbourne",
          "seat": "40"
        }
      ]
    }
  ]
}
```

