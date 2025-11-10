import {Component, inject, Input} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {MoneyTransferService} from '../../data/services/money-transfer.service';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-transfer-money-to-card-form',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './transfer-money-to-card-form.html',
  styleUrl: './transfer-money-to-card-form.scss',
})
export class TransferMoneyToCardForm {
  private fb = inject(FormBuilder)
  private moneyTransferService = inject(MoneyTransferService)

  @Input() currentCard!: GetCreditCardResponse

  form = this.fb.nonNullable.group({
    receiverCardNumber: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.pattern(/^[0-9]{16,19}$/)
    ]),
    amount: this.fb.nonNullable.control(0, [
      Validators.required,
      Validators.min(0),
      Validators.pattern(/^\d+(\.\d{1,2})?$/)
    ]),
    description: this.fb.control<string | null>(null),
    currency: this.fb.nonNullable.control('USD', [Validators.required])
  })

  onSubmit() {
    if (this.form.valid && this.currentCard) {
      const request: TransferMoneyRequest = {
        senderCardNumber: this.currentCard.cardNumber,
        receiverCardNumber: this.form.controls.receiverCardNumber.value,
        amount: this.form.controls.amount.value,
        currency: this.form.controls.currency.value,
        description: this.form.controls.description.value
      }

      this.moneyTransferService.transferMoney(request)
        .subscribe({
          error: (error: HttpErrorResponse) => {
            console.error(error)
          }
        })
    } else {
      this.form.markAllAsTouched()
    }
  }
}
