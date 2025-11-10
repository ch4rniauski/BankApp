import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {CreditCardService} from '../../data/services/credit-card.service';

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
  private creditCardService = inject(CreditCardService)

  form = this.fb.nonNullable.group({
    cardNumber: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.minLength(16),
      Validators.maxLength(19),
      Validators.pattern(/^[0-9]{16,19}$/)
    ]),
    amount: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.pattern(/^\d+(\.\d{1,2})?$/)
    ])
  })

  onSubmit() {
    if (this.form.valid) {

    } else {
      this.form.markAllAsTouched()
    }
  }
}
