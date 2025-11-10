interface TransferMoneyResponse {
  amount: number;
  currency: string;
  status: PaymentStatus;
  senderCardLast4: string;
  receiverCardLast4: string;
}
