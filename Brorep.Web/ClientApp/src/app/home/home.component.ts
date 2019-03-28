import { Component } from '@angular/core';
import {IdentityClient, CreateIdentityCommand} from '../brorep-api';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  registerUserForm: FormGroup;

  constructor(private identityClient: IdentityClient, private fb: FormBuilder) {
    this.registerUserForm = this.fb.group({
      username: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl(''),
      confirmPassword: new FormControl('')
    });
  }

  onRegisterSubmit() {
    const cmd = new CreateIdentityCommand(this.registerUserForm.value);
    this.identityClient.register(cmd).subscribe(result => {
      // todo
    }, error => console.error(error));
  }
}
