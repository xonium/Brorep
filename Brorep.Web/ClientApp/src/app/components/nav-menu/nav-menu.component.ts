import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserDto } from '../../brorep-api';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isAuth: boolean;
  user: UserDto;
  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
    this.userService.isAuthenticated.subscribe(val => {
      this.isAuth = val;
      this.user = this.userService.getCurrentUser();
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
