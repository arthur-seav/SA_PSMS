import {Component, inject} from '@angular/core';
import { FormsModule} from '@angular/forms';
import {Router} from '@angular/router';
import { Auth } from '../../services/auth';
import {HttpClient} from '@angular/common/http';


@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private auth = inject(Auth);
  private router = inject(Router);

  username = '';
  password = '';
  errorMsg = '';

  onLogin() {
    this.errorMsg = '';

    this.auth.login(this.username, this.password).subscribe({
      next: (res) => {
        this.auth.saveToken(res.token);
        this.router.navigate(['/']);
      },
      error: () => {
        this.errorMsg = 'Invalid Credentials';
      }
    });
  }
}
