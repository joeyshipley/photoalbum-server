import { describe, it, expect } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import App from './App';

describe('App', () => {
  it('increments the counter when the button is clicked', () => {
    render(<App title="React" />);

    const countButton = screen.getByTestId('count-button');
    fireEvent.click(countButton);

    // screen.debug();
    expect(countButton).toHaveTextContent('1');
  });
});