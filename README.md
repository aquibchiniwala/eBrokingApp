# eBrokingApp

## Simply run the solution file no database configurations required as inline DB is being used.

## Endpoints

## 1. Returns current portfolio and fund of the trader
### URL: https://localhost:44317/api/Trade (GET)



## 2. To add funds  to the trader wallet
### URL: https://localhost:44317/api/Trade/addfunds (POST)

(in body)

10000.00


## 3. To place a buy order
### URL: https://localhost:44317/api/Trade/buy (POST)

(in body)

{
    "EquityName": "INFY",
    "Quantity": 150,
    "TransactionDateTime": "12/22/2021 2:00PM"
}



## 4.To place a sell order
### URL:  https://localhost:44317/api/Trade/sell (POST)

(in body)

{
    "EquityName": "TCS",
    "Quantity": 10,
    "TransactionDateTime": "12/22/2021 2:00PM"
}

