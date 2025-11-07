import {Component} from '@angular/core';
import {Sidebar} from "../../common-ui/sidebar/sidebar";
import {FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-register-page',
  imports: [
    Sidebar,
    ReactiveFormsModule
  ],
  templateUrl: './register-page.html',
  styleUrl: './register-page.scss',
})
export class RegisterPage {
    form = new FormGroup({
      firstName: new FormControl(null),
      secondName: new FormControl(null),
      phoneNumber: new FormControl(null),
      email: new FormControl(null),
      password: new FormControl(null),
    })

  onsubmit(){
      console.log(this.form.value)
  }
}
