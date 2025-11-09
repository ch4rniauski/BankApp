import {Component, inject} from '@angular/core';
import {CreditCardService} from '../../data/services/credit-card.service';
import {CreditCard} from '../credit-card/credit-card';
import {HttpErrorResponse} from '@angular/common/http';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-cards-area',
  imports: [
    CreditCard,
    RouterLink
  ],
  templateUrl: './cards-area.html',
  styleUrl: './cards-area.scss',
})
export class CardsArea {
  private creditCardService = inject(CreditCardService)

  cardNumber = 0
  clientCreditCards : GetCreditCardResponse[] = []
  lastUpdateTimeArray: string[] = []

  isArrowLeftVisible = false
  isArrowRightVisible = true
  isUpdateBtnVisible = true

  ngOnInit() {
    this.creditCardService.getCreditCardsByClientId()
      .subscribe(res => {
        for (let i = 0; i < res.length; i++) {
          this.getLastUpdateTimeForCreditCard(i);
        }

        this.clientCreditCards = res
      })
  }

  getLastUpdateTimeForCreditCard(creditCardIndex: number) {
    const now = new Date()

    const day = now.getDate().toString().padStart(2, '0')
    const month = (now.getMonth() + 1).toString().padStart(2, '0')
    const year = now.getFullYear()

    const hours = now.getHours().toString().padStart(2, '0')
    const minutes = now.getMinutes().toString().padStart(2, '0')

    this.lastUpdateTimeArray[creditCardIndex] = `${day}.${month}.${year} ${hours}:${minutes}`
  }

  refreshBalanceClick() {
    this.creditCardService.getCreditCardById(this.clientCreditCards[this.cardNumber].id)
      .subscribe({
        next: (response) => {
          this.clientCreditCards[this.cardNumber] = response

          this.getLastUpdateTimeForCreditCard(this.cardNumber)
        },
        error: (err : HttpErrorResponse) => {
          console.error(err)
        }
      })
  }

  switchCreditCardArrowRightClickHandler() {
    if (this.cardNumber < this.clientCreditCards.length) {
      this.cardNumber++

      this.isArrowLeftVisible = true

      if (this.cardNumber === this.clientCreditCards.length) {
        this.isArrowRightVisible = false
        this.isUpdateBtnVisible = false
      }
    }
  }

  switchCreditCardArrowLeftClickHandler() {
    if (this.cardNumber > 0) {
      this.cardNumber--

      this.isArrowRightVisible = true
      this.isUpdateBtnVisible = true

      if (this.cardNumber === 0) {
        this.isArrowLeftVisible = false
      }
    }
  }
}
