import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(private http: HttpClient) {
  }

  onFormSubmit() {
    const data = {
      Username: '',
      Email: 'Johan',
      Password: 'Johan',
      ConfirmPassword: 'Johan',
    };

    this.http.post<CreateIdentity>('api/identity/register', data, httpOptions).subscribe(result => {

    }, error => console.error(error));

    alert('tjena');
  }
}

interface CreateIdentity {
  FirstName: string;
  LastName: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;
}
