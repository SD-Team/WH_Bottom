import { INavData } from '@coreui/angular';
import { Injectable } from '@angular/core';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
  },
  {
    name: 'Receiving Material',
    url: '/receipt/main',
    icon: 'icon-list',
  },
  {
    name: 'QR Generate',
    url: 'qr',
    icon: 'icon-frame',
    children: [
      {
        name: 'Main',
        url: '/qr/main',
        icon: 'icon-arrow-right-circle',
      },
      {
        name: 'Print',
        url: '/qr/body',
        icon: 'icon-printer',
      },
    ],
  },
  {
    name: 'Input',
    url: 'io',
    icon: 'icon-loop',
    children: [
      {
        name: 'Main',
        url: '/input/main',
        icon: 'icon-arrow-right-circle',
      },
      {
        name: 'Print Again',
        url: '/input/qrcode-again',
        icon: 'icon-printer',
      },
    ],
  },
  {
    name: 'Output',
    url: '/output/main',
    icon: 'icon-loop',
  },
  {
    name: 'History',
    url: '/transfer/history',
    icon: 'icon-speech',
  },
  {
    name: 'Rack Location',
    url: '/rack/main',
    icon: 'icon-book-open',
  },
  {
    name: 'Transfer Location',
    url: '/transfer/main',
    icon: 'icon-frame',
  },
];

@Injectable({
  providedIn: 'root'  // <- ADD THIS
})
export class NavItem {
  navItems: INavData[] = [
    {
      name: 'Dashboard',
      url: '/dashboard',
      icon: 'icon-speedometer',
    },
  ];

  constructor() { }

  getNav() {
    const user: any = JSON.parse(localStorage.getItem('user'));
    user.role.forEach((element) => {
      if (element === 'wmsb.ReceivingMaterial') {
        const navItem = {
          name: 'Receiving Material',
          url: '/receipt/main',
          icon: 'icon-list',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.QrGenerateMain') {
        const navItem = {
          name: 'QR Generate',
          url: 'qr',
          icon: 'icon-frame',
          children: [
            {
              name: 'Main',
              url: '/qr/main',
              icon: 'icon-arrow-right-circle',
            },
            {
              name: 'Print',
              url: '/qr/body',
              icon: 'icon-printer',
            },
          ],
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.InputMain') {
        const navItem = {
          name: 'Input',
          url: 'io',
          icon: 'icon-loop',
          children: [
            {
              name: 'Main',
              url: '/input/main',
              icon: 'icon-arrow-right-circle',
            },
            {
              name: 'Print Again',
              url: '/input/qrcode-again',
              icon: 'icon-printer',
            },
          ],
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.Output') {
        const navItem = {
          name: 'Output',
          url: '/output/main',
          icon: 'icon-loop',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.InOutHistory') {
        const navItem = {
          name: 'History',
          url: '/transfer/history',
          icon: 'icon-speech',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.RackLocationMain') {
        const navItem = {
          name: 'Rack Location',
          url: '/rack/main',
          icon: 'icon-book-open',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.TransferLocationMain') {
        const navItem = {
          name: 'Transfer Location',
          url: '/transfer/main',
          icon: 'icon-frame',
        };
        this.navItems.push(navItem);
      }
    });
    return this.navItems;
  }
}
