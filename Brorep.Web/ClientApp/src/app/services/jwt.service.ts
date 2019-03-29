import { Injectable } from '@angular/core';

@Injectable()
export class JwtService {

  getToken(): String {
    return window.localStorage['jwtToken'];
  }

  saveToken(token: String, date: Date) {
    window.localStorage['jwtToken'] = token; // todo: fix window reference
  }

  destroyToken() {
    window.localStorage.removeItem('jwtToken'); // todo window reference
  }

}
