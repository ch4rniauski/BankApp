interface TransferMoneyRequest {
  senderCardNumber: string;
  receiverCardNumber: string;
  amount: number;
  currency: string;
  description: string | null;
}
