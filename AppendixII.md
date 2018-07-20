## Appendix II – POST /Reservation

This endpoint allows to make a reservation into the system providing, among other parameters, the keys received with the previous call.

**This endpoint is already implemented. If you feel that it should be refactored to attend the specifications feel free to update whatever you think it should be different but keep in mind that the controller´s name and the payload should not be changed**


## Request contract in JSON format

```json
{
  "email": "contact@contact.com",
  "creditCard": "0123456789012345",
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

This request contains the email and credit card to make the reservation under, and also a list of flights that can contain one (for one way flights) or two (for roundtrip flights) elements. Every flight contains the key of the flight obtained with the GET /Flight call and a list of passengers. For every passenger we need its name, number of bags and selected seat.

## Response contract in JSON format

```json
{
 "reservationNumber": "ABC123"
}
```

The response just contains the reservation number assigned during the booking process.


