# moduler-monolith

We have three modules at this application which are Order, Stock and Payment.
We will use clean arch. for all modules. Also all modules interact each others by event driven approach via in-memory message queue.
And we will separete queries and commands in modules via MediatR using send model.
We will use single shared postgre database but we will isolate module's datas using schemas.

## use cases
 *  Customer can place an order which includes products in catalog and order will be mark as suspend status
 *  If stock is available then payment can be take
 *  If payment takes successfully then order should be mark as successed status
 *  If stock is unavailable then order should be mark as failed status
 *  Customers can observe their order's status
 *  Customers can observe stock's current state
 *  Customers can observe their payment histories
 
 
