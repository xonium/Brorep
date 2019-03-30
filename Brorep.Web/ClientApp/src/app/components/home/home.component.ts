import { Component } from '@angular/core';
import {IdentityClient, CreateIdentityCommand, CreateTokenFromIdentityCommand} from '../../brorep-api';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { JwtService } from '../../services/jwt.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  signInForm: FormGroup;
  registerUserForm: FormGroup;
  isAuth: boolean;

  constructor(private identityClient: IdentityClient, private fb: FormBuilder, private jwtService: JwtService,
    private userService: UserService) {
    this.registerUserForm = this.fb.group({
      username: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl(''),
      confirmPassword: new FormControl('')
    });

    this.signInForm = this.fb.group({
      susername: new FormControl(''),
      spassword: new FormControl('')
    });
  }

  ngOnInit(): void {
    this.userService.isAuthenticated.subscribe(val => {
      this.isAuth = val;
    });
  }

  onRegisterSubmit() {
    const cmd = new CreateIdentityCommand(this.registerUserForm.value);
    this.identityClient.register(cmd).subscribe(result => {
      // todo
    }, error => {
      console.error(error);
    });
  }

  onSignInSubmit() {
    const cmd = new CreateTokenFromIdentityCommand({
      username: this.signInForm.get('susername').value,
      password: this.signInForm.get('spassword').value
    });

    this.identityClient.createToken(cmd).subscribe(result => {
      this.jwtService.saveToken(result.token, result.expires);
      this.userService.populate();

      console.log(this.userService.getCurrentUser());
    }, error => {
      console.error(error);
    });
  }
}
