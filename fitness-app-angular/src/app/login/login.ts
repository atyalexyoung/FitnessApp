import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrls: ['./login.scss']
})
export class Login {
  username = ''
  password = ''

  constructor(private http: HttpClient) {
    console.log('Login component loaded.')
  }

  onLogin() {
    console.log('onLogin called.');

    const body = {
      username: this.username,
      password: this.password
    }

    console.log('Attempting to login with', body);

    this.http.post('https://localhost:63922/Users/login', body).subscribe({
      next: (res) => {
        console.log('Logged in!', res)
      },
      error: (err) => {
        console.error('Login failed!', err)
      }
    });
  }

  onRegister() {
    console.log('Register button clicked'); // 🔍
    const body = {
      username: this.username,
      password: this.password
    }

    console.log('Attempting to register with', body);


    this.http.post('https://localhost:63922/Users/register', body).subscribe({
      next: (res) => {
        console.log('Registered!', res)
      },
      error: (err) => {
        console.error('register failed!', err)
      }
    });
  }

}
