import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

interface BudgetEditFormProps {
  category: string;
  budget: string;
  onCategoryChange?: (value: string) => void;
  onBudgetChange?: (value: string) => void;
  onSave: (e: React.FormEvent) => void;
  onCancel: () => void;
  isLoading?: boolean;
}

export function BudgetEditForm({
  category,
  budget,
  onCategoryChange,
  onBudgetChange,
  onSave,
  onCancel,
  isLoading = false,
}: BudgetEditFormProps) {
  return (
    <form onSubmit={onSave} className='border-b pb-4 mb-4'>
      <div className='grid gap-4 md:grid-cols-2 mb-4'>
        <div>
          <label className='text-sm font-medium'>Category Name</label>
          <Input
            value={category}
            onChange={(e) => onCategoryChange?.(e.target.value)}
            required
          />
        </div>
        <div>
          <label className='text-sm font-medium'>Budget Amount</label>
          <Input
            type='number'
            value={budget}
            onChange={(e) => onBudgetChange?.(e.target.value)}
            required
          />
        </div>
      </div>
      <div className='flex gap-2'>
        <Button 
          type='submit' 
          className='bg-green-600 hover:bg-green-700' 
          size='sm'
          disabled={isLoading}
        >
          {isLoading ? 'Saving...' : 'Save'}
        </Button>
        <Button
          type='button'
          variant='outline'
          onClick={onCancel}
          size='sm'
          disabled={isLoading}
        >
          Cancel
        </Button>
      </div>
    </form>
  );
}