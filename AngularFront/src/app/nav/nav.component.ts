import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule],

  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  public accountService = inject(AccountService);
  // isLogged = false;
  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
        // this.isLogged = true;
      },
      error: error => console.log(error),
    })
  }

  logOut() {
    // this.isLogged = false;
    this.accountService.logout();
  }
}
