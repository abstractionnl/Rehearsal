import { RehearsalAppPage } from './app.po';

describe('rehearsal-app App', () => {
  let page: RehearsalAppPage;

  beforeEach(() => {
    page = new RehearsalAppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
